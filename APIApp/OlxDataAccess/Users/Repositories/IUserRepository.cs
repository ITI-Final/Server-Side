namespace OlxDataAccess.Users.Repositories
{
    using System.Threading.Tasks;

    public interface IUserRepository : IBaseRepository<User> 
    {
        Task<User> GetUserByEmail(string username);
    }
}
