namespace APIApp.Services
{
    using APIApp.Services.Authentication;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;

    public class UserAuthentication
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public UserAuthentication(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }
        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            }),
                Expires = DateTime.Now.AddDays(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await _userRepository.GetUserByEmail(username);
            if (user == null || password == null)
                return null;

            return user;
        }
    }
}
