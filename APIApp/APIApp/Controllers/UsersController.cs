using APIApp.DTOs;
using Microsoft.AspNetCore.Authorization;

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
            if (await _userAuthentication.IsEmailTakenAsync(userRegister.Email))
                return BadRequest(AppConstants.Response<string>(AppConstants.badRequestCode, AppConstants.emailIsAlreadyMessage));

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

        #region Forget Password
        //[HttpPost]
        //[Route("forgotpassword")]
        //public async Task<IActionResult> ForgotPassword([FromBody] string email)
        //{
        //    User user = await _userRepository.GetByEmail(email);

        //    if (user == null)
        //        return NotFound(AppConstants.Response<string>(AppConstants.notFoundCode, AppConstants.notFoundMessage));


        //    #region Define Claims
        //    List<Claim> claims = new List<Claim>()
        //    {
        //        new Claim(JwtRegisteredClaimNames.Sub, _configuration[key: "Jwt:Subject"]),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
        //        new Claim(ClaimTypes.Role , "User"),
        //        new Claim(type: "Id", user.Id.ToString()),
        //    };
        //    #endregion

        //    string? token = _jwt.GenentateToken(claims, 1);
        //    string? encodedToken = HttpUtility.UrlEncode(token);

        //    string? resetUrl = $"https://api/resetpassword?token={encodedToken}";

        //    // Send an email to the user with a link to the password reset page.
        //    await SendPasswordResetEmailAsync(user.Email, resetUrl);

        //    return Ok();
        //}

        //private static async Task SendPasswordResetEmailAsync(string email, string resetUrl)
        //{
        //    MailMessage? message = new MailMessage();
        //    message.From = new MailAddress(address: "olx-secuirty@olxe.com", displayName: "Security OLX");
        //    message.To.Add(email);
        //    message.Subject = "Password Reset Request";
        //    message.Body = $"Please click this link to reset your password: {resetUrl}";

        //    using (SmtpClient? client = new SmtpClient("smtp.example.com", 587))
        //    {
        //        client.Credentials = new NetworkCredential("your-email@example.com", "your-password");
        //        client.EnableSsl = true;
        //        await client.SendMailAsync(message);
        //    }
        //}
        #endregion

        #region Change Password
        [Authorize(Roles = "User")]
        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword([FromForm] ChanegPassword model, int id)
        {
            User? user = await _userRepository.GetById(id);

            if (user == null || user.Id != id)
                return NotFound(AppConstants.Response<string>(AppConstants.notFoundCode, AppConstants.notFoundMessage));

            #region Check Hashing
            if (!BCrypt.Net.BCrypt.Verify(model.OldPassword, user.Password))
                return Unauthorized(AppConstants.Response<string>(AppConstants.badRequestCode, AppConstants.passwordIsInvalid));
            #endregion

            #region Hashing new One
            string? passwordHash = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            user.Password = passwordHash;
            #endregion

            try
            {
                await _userRepository.Update(id, user);
                return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.updateSuccessMessage, 1, 1, 1, user));
            }
            catch (Exception ex)
            {
                return Problem(statusCode: AppConstants.errorCode, title: AppConstants.errorMessage);
            }
        }
        #endregion

        #endregion

        #region Get
        [Authorize(Roles = "Admin")]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            if (await _userRepository.GetAll() == null)
                return Ok(AppConstants.Response<string>(AppConstants.noContentCode, AppConstants.notContentMessage));

            User? user = await _userRepository.GetById(id);

            if (user == null)
                return NotFound(AppConstants.Response<string>(AppConstants.notFoundCode, AppConstants.notFoundMessage));

            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.getSuccessMessage, 1, 1, 1, user));
        }


        [HttpGet("id/chats")]
        public async Task<IActionResult> GetUserChats(int id)
        {
            if (await _userRepository.GetAll() == null)
                return Ok(AppConstants.Response<string>(AppConstants.noContentCode, AppConstants.notContentMessage));

            object? user = _userRepository.GetUserChats(id);

            if (user == null)
                return NotFound(AppConstants.Response<string>(AppConstants.notFoundCode, AppConstants.notFoundMessage));

            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.getSuccessMessage, 1, 1, 1, user));
        }
        #endregion

        #region Add
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUser(UserDto userDto)
        {
            if (await _userRepository.IsEmailTakenAsync(userDto.Email))
                return BadRequest(AppConstants.Response<string>(AppConstants.badRequestCode, AppConstants.emailIsAlreadyMessage));

            try
            {
                #region Hashing
                string? passwordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
                userDto.Password = passwordHash;
                #endregion

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

            if (userDto.Password == null)
            {
                User user = await _userRepository.GetById(id);

                userDto.Password = user.Password;
            }
            else
            {
                #region Hashing
                string? passwordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
                userDto.Password = passwordHash;
                #endregion
            }
            try
            {
                User? user = _mapper.Map<User>(userDto);
                await _userRepository.Update(id, user);

                return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.updateSuccessMessage, 1, 1, 1, user));
            }
            catch (Exception ex)
            {
                return Problem(statusCode: AppConstants.errorCode, title: AppConstants.errorMessage);
            }
        }
        #endregion

        #region Delete
        [Authorize(Roles = "Admin")]
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


    }
}

