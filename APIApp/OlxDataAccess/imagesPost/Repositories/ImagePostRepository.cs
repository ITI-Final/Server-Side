namespace OlxDataAccess.imagesPost.Repositories
{
    public class ImagePostRepository : BaseRepository<Post_Image>, IImagesPostRepository
    {
        private OLXContext _dbContext;
        // private IWebHostEnvironment environment;

        public ImagePostRepository(OLXContext context) : base(context)
        {
            _dbContext = context;
        }

        public override async Task<IEnumerable<Post_Image>> GetAll()
        {
            return await _dbContext.Post_Images.OrderBy(r => Guid.NewGuid()).Take(6).ToListAsync();
        }

        public async Task addmultImage(List<Post_Image> p)
        {



            foreach (var item in p)
            {
                _dbContext.Post_Images.Add(item);
            }
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<Post_Image>> getByPostId(int id)
        {
            return await _dbContext.Post_Images.Where(o => o.Post_Id == id).ToListAsync();
        }

        public async Task<string> uploadImage(IFormFile file)
        {
            var special = Guid.NewGuid().ToString();
            string hosturl = "https://localhost:7094/";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\upload\postImages", special + "-" + file.FileName);
            using (FileStream ms = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(ms);
            }
            var filename = special + "-" + file.FileName;
            //  return $"{filename}";
            return Path.Combine(@"upload\postImages", filename).ToString();
        }
        public bool DeleteImage(string imageFileName)
        {
            try
            {

                var path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", imageFileName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
