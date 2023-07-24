namespace APIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        #region Fileds
        protected readonly IPostRepository _postsReposirory;
        private readonly IMapper _mapper;
        private readonly IImagesPostRepository _imagesPostRepository;
        #endregion

        #region Constructors
        public PostsController(IPostRepository postsReposirory, IMapper mapper, IImagesPostRepository imagesPostRepository)
        {
            _postsReposirory = postsReposirory;
            _imagesPostRepository = imagesPostRepository;
            _mapper = mapper;
        }
        #endregion

        #region Methods

        #region Get

        #region Get All
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetAll(int? page, bool? isSortedByPriceAscending, string? governorate, string? city)
        {
            int? pageSize = 10;
            if (page < 1 || pageSize < 1)
                return BadRequest(AppConstants.Response<string>(AppConstants.badRequestCode, AppConstants.invalidMessage));

            int postsCount = _postsReposirory.GetAll().Result.Count();
            if (postsCount == 0)
                return Ok(AppConstants.Response<string>(AppConstants.noContentCode, AppConstants.notContentMessage));

            //IQueryable<Post>? posts = _postsReposirory.GetAllWithSorting(page: page ?? 1, pageSize: pageSize ?? postsCount, isSortedByPriceAscending); 

            IQueryable<Post>? posts = _postsReposirory.GetPostsInCity(page: page ?? 1, pageSize: pageSize ?? postsCount, isSortedByPriceAscending, governorate, city);

            int totalPages = (int)Math.Ceiling((double)postsCount / pageSize ?? postsCount);
            if (totalPages < page)
                return BadRequest(AppConstants.Response<string>(AppConstants.badRequestCode, AppConstants.invalidMessage));

            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.getSuccessMessage, page ?? 1, totalPages, postsCount, posts));
        }
        #endregion

        #region Get By Id
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Post>> GetById(int id)
        //{
        //    Post post = await _postsReposirory.GetById(id);

        //    if (post == null)
        //        return Ok(AppConstants.Response<string>(AppConstants.noContentCode, AppConstants.notContentMessage));

        //    #region serializer for fields value
        //    //[{ "fieldID": 3, "Choices": [1, 2] }]
        //    //List<FieldValuesDTO> fieldvalue = JsonSerializer.Deserialize<List<FieldValuesDTO>>(post.Fields)!;
        //    #endregion

        //    #region return field and it's value
        //    //List<returnFieldDTO> f = new List<returnFieldDTO>();
        //    //List<Field> fields = post.Cat.Fields.Where(o =>
        //    //{
        //    //    foreach (var item in fieldvalue)
        //    //    {
        //    //        if (o.Id == item.fieldID)
        //    //        {
        //    //            return true;
        //    //        }
        //    //    }
        //    //    return false;
        //    //}).ToList();
        //    //foreach (var item in fields)
        //    //{
        //    //    List<Choice> choices = item.Choices.Where(o =>
        //    //    {
        //    //        foreach (var item1 in fieldvalue)
        //    //        {
        //    //            foreach (var item2 in item1.choices)
        //    //            {
        //    //                if (o.Id == item2)
        //    //                {
        //    //                    return true;
        //    //                }
        //    //            }
        //    //        }
        //    //        return false;

        //    //    }).ToList();
        //    //    List<returnChoicesDTO> c = new List<returnChoicesDTO>();
        //    //    foreach (var item1 in choices)
        //    //    {
        //    //        returnChoicesDTO returnChoices = new returnChoicesDTO()
        //    //        {
        //    //            Id = item1.Id,
        //    //            Label = item1.Label,
        //    //            Label_Ar = item1.Label_Ar,
        //    //        };
        //    //        c.Add(returnChoices);
        //    //    }
        //    //    returnFieldDTO returnFieldDTO = new returnFieldDTO()
        //    //    {
        //    //        Field_Id = item.Id,
        //    //        Field_Name = item.Name,
        //    //        Field_Label = item.Label,
        //    //        Field_Label_Ar = item.Label_Ar,
        //    //        Choices = c,

        //    //    };
        //    //    f.Add(returnFieldDTO);

        //    //}
        //    #endregion

        //    #region return post with with it's chocien value
        //    //GovernoratePostDTO g = new GovernoratePostDTO()
        //    //{
        //    //    Id = post.Post_LocationNavigation.Governorate.Id,
        //    //    Governorate_Name_En = post.Post_LocationNavigation.Governorate.Governorate_Name_En,
        //    //    Governorate_Name_Ar = post.Post_LocationNavigation.Governorate.Governorate_Name_Ar,

        //    //};
        //    //CityPostDTO cities = new CityPostDTO()
        //    //{
        //    //    City_Name_Ar = post.Post_LocationNavigation.City_Name_Ar,
        //    //    City_Name_En = post.Post_LocationNavigation.City_Name_En,
        //    //    Id = post.Post_LocationNavigation.Id,
        //    //    Governorate = g,

        //    //};
        //    #endregion

        //    #region return post with with it's chocien value
        //    GovernoratePostDTO g = new GovernoratePostDTO()
        //    {
        //        Id = post.Post_LocationNavigation.Governorate.Id,
        //        Governorate_Name_En = post.Post_LocationNavigation.Governorate.Governorate_Name_En,
        //        Governorate_Name_Ar = post.Post_LocationNavigation.Governorate.Governorate_Name_Ar,

        //    };
        //    CityPostDTO cities = new CityPostDTO()
        //    {
        //        City_Name_Ar = post.Post_LocationNavigation.City_Name_Ar,
        //        City_Name_En = post.Post_LocationNavigation.City_Name_En,
        //        Id = post.Post_LocationNavigation.Id,
        //        Governorate = g,

        //    };
        //    #endregion

        //    List<GetImagesPostDTO> i = new List<GetImagesPostDTO>();
        //    foreach (var item in post.Post_Images)
        //    {
        //        item.
        //        Id = post.Post_LocationNavigation.Governorate.Id,
        //        Governorate_Name_En = post.Post_LocationNavigation.Governorate.Governorate_Name_En,
        //        Governorate_Name_Ar = post.Post_LocationNavigation.Governorate.Governorate_Name_Ar,

        //    };
        //    CityPostDTO cities = new CityPostDTO()
        //    {
        //        City_Name_Ar = post.Post_LocationNavigation.City_Name_Ar,
        //        City_Name_En = post.Post_LocationNavigation.City_Name_En,
        //        Id = post.Post_LocationNavigation.Id,
        //        Governorate = g,
        //        Cat_Id = post.Cat_Id,
        //        Id = post.Id,
        //        Price = post.Price,
        //        Contact_Method = post.Contact_Method,
        //        Created_Date = post.Created_Date,
        //        Description = post.Description,
        //        Fields = f,
        //        Post_LocationNavigation = cities,
        //        Post_Image = i,
        //        Is_Special = post.Is_Special,
        //        Is_Visible = post.Is_Visible,
        //        Post_Location = post.Post_Location,
        //        Price_Type = post.Price_Type,
        //        User_Id = post.User_Id,
        //        Title = post.Title,
        //        Views = post.Views
        //    };


        //    return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.getSuccessMessage, 1, 1, 1, post));
        //}
        #endregion

        #region Get User Posts
        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetByUserId(int id)
        {
            int? pageSize = 10;

            int postsCount = _postsReposirory.GetByUserId(id).Result.Count();
            if (postsCount == 0)
                return Ok(AppConstants.Response<string>(AppConstants.noContentCode, AppConstants.notContentMessage));

            var posts = await _postsReposirory.GetByUserId(id);



            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.getSuccessMessage, 1, 0, postsCount, posts));
        }
        #endregion

        #endregion

        #endregion

        #region Add
        [HttpPost]
        public async Task<ActionResult> Add([FromForm] PostDTO postDTO)
        {


            if (postDTO == null)
            {
                return BadRequest(AppConstants.Response<string>(AppConstants.badRequestCode, AppConstants.invalidMessage));
            }

            foreach (var fieldDTO in postDTO.Post_Images)
            {
                fieldDTO.Image = await _imagesPostRepository.uploadImage(fieldDTO.ImageFile);
            }

            Post? post = _mapper.Map<Post>(postDTO);
            await _postsReposirory.Add(post);

            return Created("", AppConstants.Response<object>(AppConstants.successCode, AppConstants.addSuccessMessage, 1, 1, 1, post));

        }
        #region saveImage

        //[NonAction]
        //public async Task<string> uploadImage(IFormFile file)
        //{
        //    var special = Guid.NewGuid().ToString();
        //    string hosturl = "https://localhost:7094/";
        //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\upload\postImages", special + "-" + file.FileName);
        //    using (FileStream ms = new FileStream(filePath, FileMode.Create))
        //    {
        //        file.CopyToAsync(ms);
        //    }
        //    var filename = special + "-" + file.FileName;
        //    //  return $"{filename}";
        //    return Path.Combine(hosturl, @"upload\postImages", filename).ToString();
        //}
        #endregion
        #endregion

        #region Update
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, PostDTO postDto)
        {

            if (id != postDto.Id)
                return NotFound(AppConstants.Response<string>(AppConstants.notFoundCode, AppConstants.notFoundMessage));

            Post? post = _mapper.Map<Post>(postDto);
            try
            {
                await _postsReposirory.Update(id, post);
            }
            catch (DbUpdateConcurrencyException e)
            {
                return Problem(statusCode: AppConstants.errorCode, title: AppConstants.errorMessage);
            }

            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.updateSuccessMessage, 1, 1, 1, post));
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Post? post = await _postsReposirory.GetById(id);

            ICollection<Post_Image> p = post.Post_Images;
            foreach (var item in p)
            {
                _imagesPostRepository.DeleteImage(item.Image);
            }

            if (post == null)
                return NotFound(AppConstants.Response<string>(AppConstants.notFoundCode, AppConstants.notFoundMessage));

            try
            {
                await _postsReposirory.DeleteById(id);
                return Ok(AppConstants.Response<string>(AppConstants.successCode, AppConstants.deleteSuccessMessage));

            }
            catch (Exception e)
            {
                return Problem(statusCode: AppConstants.errorCode, title: AppConstants.errorMessage);
                //return Problem(statusCode: 500, title: e.Message);
            }
        }
        #endregion

    }
}
