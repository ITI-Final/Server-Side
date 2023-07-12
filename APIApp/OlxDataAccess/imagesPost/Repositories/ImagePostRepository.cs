using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlxDataAccess.imagesPost.Repositories
{
    public class ImagePostRepository : BaseRepository<Post_Image>, IImagesPostRepository
    {
        private OLXContext _dbContext;
        public ImagePostRepository(OLXContext context) : base(context)
        {
            _dbContext = context;
        }

    }
}
