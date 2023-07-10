using APIApp.DTOs.FavouriteDTOs;
using OlxDataAccess.Favourits.FavouritRepositories;

namespace APIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouritesController : ControllerBase
    {

        #region Fileds
        protected readonly IFavouriteRepositort _favouriteRepositort;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public FavouritesController(IFavouriteRepositort favouriteRepositort, IMapper mapper)
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
            IEnumerable<Favorite> favorites = await _favouriteRepositort.GetAll();
            if (favorites.Count() < 1)
                return Ok(AppConstants.Response<string>(AppConstants.noContentCode, AppConstants.notContentMessage));

            //return Ok(await _favouriteRepositort.GetAll());
            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.getSuccessMessage, favorites));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Favorite>> GetById(int id)
        {
            Favorite favourite = await _favouriteRepositort.GetById(id);

            //return Ok(favourite);
            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.getSuccessMessage, favourite));

        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult> Add(FavouriteDTO favouriteDTO)
        {
            if (favouriteDTO == null)
                //return BadRequest(AppConstants.GetBadRequest());
                return BadRequest(AppConstants.Response<string>(AppConstants.badRequestCode, AppConstants.invalidMessage));

            #region autoMapper
            Favorite? favorite = _mapper.Map<Favorite>(favouriteDTO);
            #endregion
            await _favouriteRepositort.Add(favorite);

            // return CreatedAtAction("GetAll", new { id = favouriteDTO.Id }, favouriteDTO);
            return Created("GetCategories", AppConstants.Response<object>(AppConstants.successCode, AppConstants.addSuccessMessage, favouriteDTO));
        }

        #endregion

        #region Update

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, FavouriteDTO favouriteDTO)
        {

            if (id != favouriteDTO.Id)
                //return NotFound();
                return NotFound(AppConstants.Response<string>(AppConstants.notFoundCode, AppConstants.notFoundMessage));


            var favorite = _mapper.Map<Favorite>(favouriteDTO);
            await _favouriteRepositort.Update(id, favorite);

            //return NoContent();
            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.updateSuccessMessage, favorite));

        }
        #endregion

        #region delete

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _favouriteRepositort.DeleteById(id);

            //return Ok();
            return Ok(AppConstants.Response<string>(AppConstants.successCode, AppConstants.deleteSuccessMessage));
        }
        #endregion
    }
}
