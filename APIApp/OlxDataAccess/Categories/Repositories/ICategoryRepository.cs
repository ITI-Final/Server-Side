namespace OlxDataAccess.Categories.Repositories
{

    public interface ICategoryRepository : IBaseRepository<Category>
    {
        public Task<Category> GetCategoryWithPosts(string slug);
    }

}
