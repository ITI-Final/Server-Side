namespace OlxDataAccess.Users.Repositories
{
    public interface IUserRepository : IBaseRepository<User>, IAuthentication<User>
    {
        Task<User> GetByEmail(string email);

        Task Register(User user);
        object GetUserChats(int id);
    }
}
