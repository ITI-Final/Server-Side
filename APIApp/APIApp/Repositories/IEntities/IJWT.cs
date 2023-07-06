using System.Security.Claims;

namespace APIApp.Repositories.IEntities
{
    public interface IJWT
    {
        public string GenentateToken(ICollection<Claim> claims);
    }
}
