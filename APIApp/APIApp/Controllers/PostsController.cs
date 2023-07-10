using APIApp.DTOs.PostsDTOs;
using OlxDataAccess.Posts.Repositories;
using System.Text.Json;

namespace APIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        #region Fileds
        protected readonly IPostRepository _postsReposirory;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public PostsController(IPostRepository postsReposirory, IMapper mapper)
        {
            _postsReposirory = postsReposirory;
            _mapper = mapper;
        }
        #endregion

        #region Methods

        #region Get

        #region Get All
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetAll()
        {
            IEnumerable<Post> posts = await _postsReposirory.GetAll();
            if (posts.Count() < 1)
                return Ok(AppConstants.Response<string>(AppConstants.noContentCode, AppConstants.notContentMessage));

            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.getSuccessMessage, posts));
        }
        #endregion

        #region Get By Id

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> getById(int id)
        {
            Post post = await _postsReposirory.GetById(id);

            if (post == null)
                return Ok(AppConstants.Response<string>(AppConstants.noContentCode, AppConstants.notContentMessage));

            #region serializer for fields value
            //[{ "fieldID": 3, "choices": [1, 2] }]
            List<FieldValuesDTO> fieldvalue = JsonSerializer.Deserialize<List<FieldValuesDTO>>(post.Fields)!;
            #endregion

            #region return field and it's value
            List<returnFieldDTO> f = new List<returnFieldDTO>();
            List<Field> fields = post.Cat.Fields.Where(o =>
            {
                foreach (var item in fieldvalue)
                {
                    if (o.Id == item.fieldID)
                    {
                        return true;
                    }
                }
                return false;
            }).ToList();
            foreach (var item in fields)
            {
                List<Choice> choices = item.Choices.Where(o =>
                {
                    foreach (var item1 in fieldvalue)
                    {
                        foreach (var item2 in item1.choices)
                        {
                            if (o.Id == item2)
                            {
                                return true;
                            }
                        }
                    }
                    return false;

                }).ToList();
                List<returnChoicesDTO> c = new List<returnChoicesDTO>();
                foreach (var item1 in choices)
                {
                    returnChoicesDTO returnChoices = new returnChoicesDTO()
                    {
                        Id = item1.Id,
                        Label = item1.Label,
                        Label_Ar = item1.Label_Ar,
                    };
                    c.Add(returnChoices);
                }
                returnFieldDTO returnFieldDTO = new returnFieldDTO()
                {
                    Field_Id = item.Id,
                    Field_Name = item.Name,
                    Field_Label = item.Label,
                    Field_Label_Ar = item.Label_Ar,
                    choices = c,

                };
                f.Add(returnFieldDTO);

            }
            #endregion

            #region return post with with it's chocien value
            PostGetDTO postGetDTO = new PostGetDTO()
            {
                Cat_Id = post.Cat_Id,
                Id = post.Id,
                Price = post.Price,
                Contact_Method = post.Contact_Method,
                Created_Date = post.Created_Date,
                Description = post.Description,
                Fields = f,
                Is_Special = post.Is_Special,
                Is_Visible = post.Is_Visible,
                Post_Location = post.Post_Location,
                Price_Type = post.Price_Type,
                User_Id = post.User_Id,
                Title = post.Title,
                Views = post.Views

            };

            #endregion

            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.getSuccessMessage, postGetDTO));
        }
        #endregion

        #endregion

        #region Add
        [HttpPost]
        public async Task<ActionResult> Add(PostDTO postDTO)
        {

            if (postDTO == null)
                return BadRequest(AppConstants.Response<string>(AppConstants.badRequestCode, AppConstants.invalidMessage));

            Post? post = _mapper.Map<Post>(postDTO);
            await _postsReposirory.Add(post);

            return Created("", AppConstants.Response<object>(AppConstants.successCode, AppConstants.addSuccessMessage, post));

        }
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

            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.updateSuccessMessage, post));
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Post? post = await _postsReposirory.GetById(id);
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

        #endregion
    }
}
