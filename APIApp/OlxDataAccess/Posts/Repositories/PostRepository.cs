namespace OlxDataAccess.Posts.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(OLXContext context) : base(context)
        {
        }
    }
}
