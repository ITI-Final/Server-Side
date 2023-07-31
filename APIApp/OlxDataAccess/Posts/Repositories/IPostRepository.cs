namespace OlxDataAccess.Posts.Repositories
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Task<IEnumerable<Post>> GetByUserId(int id);
        IQueryable<Post> GetAllWithSorting(int page, int pageSize, bool? isSortingAsc);
        IQueryable<Post> GetPostsInCity(int page, int pageSize, bool? isSortingAsc, int? governorate, int? city);
    }
}
