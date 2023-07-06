using APIApp.Repositories.IEntities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIApp.Repositories.JWT
{
    public class JWTRepository : IJWT
    {

        #region Fields
        IConfiguration _configuration;

        #endregion           

        #region Constructors
        public JWTRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Methods
        public string GenentateToken(ICollection<Claim> claims)
        {
            #region Secret Key
            SymmetricSecurityKey secritKey =
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(s: _configuration["Jwt:Key"]));
            #endregion

            SigningCredentials? signingCredentials = new SigningCredentials(secritKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken? jwtSecurityToken = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.Now.AddDays(1)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
        #endregion      

    }
}
