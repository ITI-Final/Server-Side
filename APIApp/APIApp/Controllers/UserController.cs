namespace APIApp.Controllers
{
    using APIApp.DTOs.UserDTOs;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        protected readonly IUserRepository _userRepository;
        protected readonly IMapper _mapper;
        protected readonly IAuthentication<User> _userAuthentication;
        protected readonly IConfiguration _configuration;
        protected readonly IJWT _jwt;


        public UserController(IUserRepository userRepository, IMapper mapper, IAuthentication<User> userAuthentication, IConfiguration configuration, IJWT jwt)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userAuthentication = userAuthentication;
            _configuration = configuration;
            _jwt = jwt;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return Ok(await _userRepository.GetAll());
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetUserById(int id)
        {
            return Ok(await _userRepository.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserDto userDto)
        {
            try
            {
                if (await _userRepository.IsEmailTakenAsync(userDto.Email)) return BadRequest(AppConstants.GetEmailFound());
                var user = _mapper.Map<User>(userDto);
                await _userRepository.Add(user);

                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the user.");
            }

        }
        [HttpPut("id")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto userDto, int id)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                await _userRepository.Update(id, user);

                return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error occurred while Updation the user.");
            }
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return Ok(_userRepository.DeleteById(id));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] UserLoginDTO userLoginDTO)
        {

            #region Check Parameters 
            if (userLoginDTO.Email == null || userLoginDTO.Password == null) return BadRequest(AppConstants.GetBadRequest());
            #endregion

            User? user = await _userRepository.Login(userLoginDTO.Email, userLoginDTO.Password);

            #region Check is Existed

            if (user == null)
                return BadRequest(AppConstants.GetBadRequest());
            #endregion

            #region Define Claims
            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration[key: "Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(type: "name", user.Name),
                new Claim(ClaimTypes.Role , "User"),
                new Claim(type: "Id", user.Id.ToString()),
            };
            #endregion

            #region Response Formatter
            object response = AppConstants.UserLoginSuccessfully(userLoginDTO, _jwt.GenentateToken(claims, numberOfDays: 1));

            #endregion


            return Ok(response);
        }
    }
}
