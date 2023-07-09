namespace APIApp.Controllers
{
    using APIApp.DTOs.UserDTOs;
    using APIApp.Services;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        protected readonly IUserRepository _userRepository;
        protected readonly IMapper _mapper;
        protected readonly UserAuthentication _userAuthentication;

        public UserController(IUserRepository userRepository, IMapper mapper, UserAuthentication userAuthentication)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userAuthentication = userAuthentication;
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
        public async Task<IActionResult> AddUser( UserDto userDto)
        {
            try
            {
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
        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
        {
            var user = _mapper.Map<User>(userLoginDTO);

             await _userAuthentication.Authenticate(user.Email, user.Password);

            if (user == null)
                return Unauthorized();

            var token =_userAuthentication.GenerateJwtToken(user);
            //return Ok(new { Token = token });
            return Ok(AppConstants.UserLoginSuccessfully(userLoginDTO, token));
        }
    }
}
