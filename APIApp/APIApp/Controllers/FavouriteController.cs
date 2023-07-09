using APIApp.DTOs.FavouriteDTOs;
using OlxDataAccess.Favourits.FavouritRepositories;

namespace APIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteController : ControllerBase
    {

        #region Fileds
        protected readonly IFavouriteRepositort _favouriteRepositort;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public FavouriteController(IFavouriteRepositort favouriteRepositort, IMapper mapper)
        {
            _favouriteRepositort = favouriteRepositort;
            _mapper = mapper;

        }
        #endregion

        // GET: api/Categories
        #region get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Favorite>>> GetAll()
        {
            return Ok(await _favouriteRepositort.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Favorite>> GetById(int id)
        {
            Favorite favourite = await _favouriteRepositort.GetById(id);

            return Ok(favourite);
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult> Add(FavouriteDTO favouriteDTO)
        {
            if (favouriteDTO == null)
                return BadRequest(AppConstants.GetBadRequest());


            #region autoMapper
            Favorite? favorite = _mapper.Map<Favorite>(favouriteDTO);
            #endregion
            await _favouriteRepositort.Add(favorite);
            return CreatedAtAction("GetAll", new { id = favouriteDTO.Id }, favouriteDTO);


        }

        #endregion

        #region Update

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, FavouriteDTO favouriteDTO)
        {

            if (id != favouriteDTO.Id)
            {
                return NotFound();
            }

            var favorite = _mapper.Map<Favorite>(favouriteDTO);
            await _favouriteRepositort.Update(id, favorite);
            return NoContent();
        }
        #endregion

        #region delete

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _favouriteRepositort.DeleteById(id);
            return Ok();
        }
        #endregion
    }
}
