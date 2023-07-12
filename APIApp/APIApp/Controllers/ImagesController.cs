using APIApp.DTOs.PostsDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OlxDataAccess.imagesPost.Repositories;

namespace APIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private IImagesPostRepository _imagesPostRepository;
        private IMapper _mapper;

        public ImagesController(IImagesPostRepository imagesPostRepository, IMapper mapper)
        {
            _imagesPostRepository = imagesPostRepository;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult> addimage([FromForm] imagesDTO imagesDTO)
        {
            imagesDTO.Image = uploadImage(imagesDTO.ImageFile);
            var image = _mapper.Map<Post_Image>(imagesDTO);
            await _imagesPostRepository.Add(image);
            return Ok(image);
        }
        #region saveImage
        [NonAction]
        public string uploadImage(IFormFile file)
        {
            var special = Guid.NewGuid().ToString();
            string hosturl = "https://localhost:7094/";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\upload\postImages", special + "-" + file.FileName);
            using (FileStream ms = new FileStream(filePath, FileMode.Create))
            {
                file.CopyToAsync(ms);
            }
            var filename = special + "-" + file.FileName;
            //  return $"{filename}";
            return Path.Combine(hosturl, @"upload\postImages", filename).ToString();
        }
        #endregion

    }
}
