using System.Security.Claims;

namespace APIApp.Services.JWT
{
    public interface IJWT
    {
        public string GenentateToken(ICollection<Claim> claims, int numberOfDays);
    }
}
