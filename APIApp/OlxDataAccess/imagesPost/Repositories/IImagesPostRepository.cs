using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlxDataAccess.imagesPost.Repositories
{
    public interface IImagesPostRepository : IBaseRepository<Post_Image>
    {
        public Task addmultImage(List<Post_Image> p);

        public Task<List<Post_Image>> getByPostId(int id);
    }
}
