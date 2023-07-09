namespace OlxDataAccess.Users.Repositories
{
    public interface IUserRepository : IBaseRepository<User>, IAuthentication<User>
    {

    }
}
