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
            return Ok(await _postsReposirory.GetAll());
        }
        #endregion

        #region Get By Id

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> getById(int id)
        {
            Post post = await _postsReposirory.GetById(id);
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

            return Ok(postGetDTO);

        }
        #endregion

        #endregion

        #region Add
        [HttpPost]
        public async Task<ActionResult> Add(PostDTO postDTO)
        {

            if (postDTO == null)
            {
                return BadRequest();
            }
            Post? post = _mapper.Map<Post>(postDTO);
            await _postsReposirory.Add(post);
            return Ok();
        }
        #endregion

        #region Update
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, PostDTO postDto)
        {

            if (id != postDto.Id)
            {
                return NotFound();
            }

            Post? post = _mapper.Map<Post>(postDto);

            await _postsReposirory.Update(id, post);
            return Ok(AppConstants.UpdatedSuccessfully());
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _postsReposirory.DeleteById(id);
            return Ok(AppConstants.DeleteSuccessfully());

        }
        #endregion

        #endregion
    }
}
