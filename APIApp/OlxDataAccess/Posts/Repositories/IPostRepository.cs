namespace OlxDataAccess.Posts.Repositories
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Task<IEnumerable<Post>> GetByUserId(int id);
    }
}
