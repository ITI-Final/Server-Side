using System.Dynamic;

namespace OlxDataAccess.Categories.Repositories
{





    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly OLXContext _dbContext;
        public CategoryRepository(OLXContext context) : base(context)
        {
            _dbContext = context;
        }
        public override async Task<IEnumerable<Category>> GetAll()
        {
            return await _dbContext.Categories.Include(o => o.Posts).ThenInclude(o => o.Post_Images).Include(q => q.Fields).ThenInclude(a => a.Choices).Include(o => o.InverseParent).ThenInclude(o => o.InverseParent).ToListAsync();

        }
        public async override Task<Category> GetById(int id)
        {

            return await _dbContext.Categories.Include(q => q.Fields).ThenInclude(a => a.Choices).Include(o => o.InverseParent).ThenInclude(o => o.InverseParent).FirstOrDefaultAsync(o => o.Id == id);
        }
        public async Task<Category> GetCategoryWithPosts(string slug)
        {
            return await _dbContext.Categories.Include(o => o.Posts).ThenInclude(o => o.Post_Images).FirstOrDefaultAsync(o => o.Slug == slug);
        }

    }
}
