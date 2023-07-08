using APIApp.Services.Authentication;
using OlxDataAccess.Models;

namespace OlxDataAccess.Admins.Repository
{
    public interface IAdminRepository : IBaseRepository<Admin>, IAuthentication<Admin>
    {
    }
}
