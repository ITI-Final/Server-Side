namespace OlxDataAccess.Categories.Repositories
{
    using OlxDataAccess;
    using OlxDataAccess.DBContext;
    using OlxDataAccess.Models;

    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(OLXContext context) : base(context)
        {
        }
    }
}
