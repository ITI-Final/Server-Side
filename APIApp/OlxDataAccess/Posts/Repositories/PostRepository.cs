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

        public async Task<IEnumerable<Post>> GetByUserId(int id)
        {
            return await _context.Posts.Include(o => o.Post_Images).Include(o => o.Post_LocationNavigation).ThenInclude(o => o.Governorate).Include(o => o.Cat).Where(o => o.User_Id == id).ToListAsync();
        }

        public IQueryable<Post> GetAllWithSorting(int page, int pageSize, bool? isSortingAsc)
        {
            if (isSortingAsc.HasValue)
                if (isSortingAsc == true)
                    return _context.Posts.Include(p => p.Post_LocationNavigation).ThenInclude(o => o.Governorate).OrderBy(p => p.Price).Skip((page - 1) * pageSize).Take(pageSize);
                else
                    return _context.Posts.Include(p => p.Post_LocationNavigation).ThenInclude(o => o.Governorate).OrderByDescending(p => p.Price).Skip((page - 1) * pageSize).Take(pageSize);

            return _context.Posts
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(p => p.Post_Images)
                .Include(p => p.Post_LocationNavigation);
        }

        public IQueryable<Post> GetPostsInCity(int page, int pageSize, bool? isSortingAsc, int? governorateId, int? cityId)
        {
            IQueryable<Post> posts = _context.Posts;

            if (governorateId != null)
                posts.Where(p => p.Post_LocationNavigation.Governorate.Id == governorateId);

            if (cityId != null)
                posts.Where(p => p.Post_LocationNavigation.Id == cityId);

            if (isSortingAsc.HasValue)
                if (isSortingAsc == true)
                    return posts.Include(p => p.Post_LocationNavigation).ThenInclude(o => o.Governorate).OrderBy(p => p.Price).Skip((page - 1) * pageSize).Take(pageSize);
                else
                    return posts.Include(p => p.Post_LocationNavigation).ThenInclude(o => o.Governorate).OrderByDescending(p => p.Price).Skip((page - 1) * pageSize).Take(pageSize);

            return posts
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(p => p.Post_Images)
                .Include(p => p.Post_LocationNavigation)
                .ThenInclude(o => o.Governorate);
        }

    }
}
