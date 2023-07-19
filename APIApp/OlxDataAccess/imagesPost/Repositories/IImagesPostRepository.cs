namespace OlxDataAccess.imagesPost.Repositories
{
    public interface IImagesPostRepository : IBaseRepository<Post_Image>
    {
        public Task addmultImage(List<Post_Image> p);

        public Task<List<Post_Image>> getByPostId(int id);

        public Task<string> uploadImage(IFormFile file);
        public bool DeleteImage(string imageFileName);
    }
}
