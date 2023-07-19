namespace APIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Fileds
        protected readonly IUserRepository _userRepository;
        protected readonly IMapper _mapper;
        protected readonly IAuthentication<User> _userAuthentication;
        protected readonly IConfiguration _configuration;
        protected readonly IJWT _jwt;
        #endregion

        #region Constructors
        public UsersController(IUserRepository userRepository, IMapper mapper, IAuthentication<User> userAuthentication, IConfiguration configuration, IJWT jwt)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userAuthentication = userAuthentication;
            _configuration = configuration;
            _jwt = jwt;
        }
        #endregion

        #region Methods

        #region Authentication

        #region Login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] UserLoginDTO userLoginDTO)
        {

            User? user = await _userRepository.Login(userLoginDTO.Email);

            #region Check is Existed
            if (user == null)
                return NotFound(AppConstants.Response<string>(AppConstants.notFoundCode, AppConstants.notFoundMessage));
            #endregion

            #region Check Hashing
            if (!BCrypt.Net.BCrypt.Verify(userLoginDTO.Password, user.Password))
                return Unauthorized(AppConstants.Response<string>(AppConstants.badRequestCode, AppConstants.passwordIsInvalid));
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


            return Ok(AppConstants.LoginSuccessfully(userLoginDTO, _jwt.GenentateToken(claims, numberOfDays: 1)));
        }
        #endregion

        #region Register

        [HttpPost("register")]
        public async Task<IActionResult> AddUser(UserRegister userRegister)
        {
            try
            {
                #region Hashing
                string? passwordHash = BCrypt.Net.BCrypt.HashPassword(userRegister.Password);
                userRegister.Password = passwordHash;
                #endregion

                User user = _mapper.Map<User>(userRegister);

                await _userRepository.Add(user);

                return Created("", AppConstants.Response<object>(AppConstants.successCode, AppConstants.addSuccessMessage, 1, 1, 1, user));
            }
            catch (Exception ex)
            {
                return Problem(statusCode: AppConstants.errorCode, title: AppConstants.errorMessage);
            }

        }
        #endregion

        #endregion

        #region Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers(int? page)
        {
            int? pageSize = 10;
            if (page < 1 || pageSize < 1)
                return BadRequest(AppConstants.Response<string>(AppConstants.badRequestCode, AppConstants.invalidMessage));

            int usersCount = _userRepository.GetAll().Result.Count();
            if (usersCount == 0)
                return Ok(AppConstants.Response<string>(AppConstants.noContentCode, AppConstants.notContentMessage));

            IEnumerable<User> users = await _userRepository.GetAllWithPagination(page: page ?? 1, pageSize: pageSize ?? usersCount);

            int totalPages = (int)Math.Ceiling((double)usersCount / pageSize ?? usersCount);
            if (totalPages < page)
                return BadRequest(AppConstants.Response<string>(AppConstants.badRequestCode, AppConstants.invalidMessage));

            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.getSuccessMessage, page ?? 1, totalPages, usersCount, users));

        }

        [HttpGet("id")]
        public async Task<IActionResult> GetUserById(int id)
        {
            if (await _userRepository.GetAll() == null)
                return Ok(AppConstants.Response<string>(AppConstants.noContentCode, AppConstants.notContentMessage));

            User? user = await _userRepository.GetById(id);

            if (user == null)
                return NotFound(AppConstants.Response<string>(AppConstants.notFoundCode, AppConstants.notFoundMessage));

            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.getSuccessMessage, 1, 1, 1, user));
        }
        #endregion

        #region Add
        [HttpPost]
        public async Task<IActionResult> AddUser(UserDto userDto)
        {
            if (await _userRepository.IsEmailTakenAsync(userDto.Email))
                return BadRequest(AppConstants.Response<string>(AppConstants.badRequestCode, AppConstants.emailIsAlreadyMessage));

            try
            {
                User? user = _mapper.Map<User>(userDto);
                await _userRepository.Add(user);

                return Created("", AppConstants.Response<object>(AppConstants.successCode, AppConstants.addSuccessMessage, 1, 1, 1, user));
            }
            catch (Exception ex)
            {
                return Problem(statusCode: AppConstants.errorCode, title: AppConstants.errorMessage);
            }

        }
        #endregion

        #region Update
        [HttpPut("id")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto userDto, int id)
        {
            if (id != userDto.Id)
                return NotFound(AppConstants.Response<string>(AppConstants.notFoundCode, AppConstants.notFoundMessage));

            try
            {
                User? user = _mapper.Map<User>(userDto);
                await _userRepository.Update(id, user);

                return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.updateSuccessMessage, 1, 1, 1, user));
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error occurred while Updation the user.");
            }
        }
        #endregion

        #region Delete

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            User? user = await _userRepository.GetById(id);

            if (user == null)
                return NotFound(AppConstants.Response<string>(AppConstants.notFoundCode, AppConstants.notFoundMessage));

            try
            {
                await _userRepository.DeleteById(id);
                return Ok(AppConstants.Response<string>(AppConstants.successCode, AppConstants.deleteSuccessMessage));
            }
            catch (Exception e)
            {
                return Problem(statusCode: 500, title: e.Message);
            }
        }
        #endregion

        #endregion
    }
}
