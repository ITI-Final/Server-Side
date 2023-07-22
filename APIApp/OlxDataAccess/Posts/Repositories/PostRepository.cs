namespace OlxDataAccess.Posts.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        private OLXContext _context;
        public PostRepository(OLXContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<Post> GetById(int id)
        {
            return await _context.Posts.Include(o => o.Post_Images).Include(o => o.Post_LocationNavigation).ThenInclude(o => o.Governorate).Include(o => o.Cat).ThenInclude(o => o.Fields).ThenInclude(o => o.Choices).FirstOrDefaultAsync(o => o.Id == id);
        }
        public override async Task<IEnumerable<Post>> GetAll()
        {
            return await _context.Posts.Include(o => o.Post_Images).ToListAsync();
        }
    }
}
