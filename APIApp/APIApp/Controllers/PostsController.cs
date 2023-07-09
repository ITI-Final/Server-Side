using APIApp.DTOs.PostsDTOs;
using OlxDataAccess.Posts.Repositories;

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

            return Ok(await _postsReposirory.GetById(id));

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
