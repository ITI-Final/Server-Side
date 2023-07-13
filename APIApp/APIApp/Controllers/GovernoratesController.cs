using OlxDataAccess.Governorates.Repositories;

namespace APIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GovernoratesController : ControllerBase
    {
        #region fileds
        private IGovernorateRepository _governorateRepository;
        private IMapper _mapper;
        #endregion

        #region ctor
        public GovernoratesController(IGovernorateRepository governorateRepository, IMapper mapper)
        {
            _governorateRepository = governorateRepository;
            _mapper = mapper;
        }
        #endregion

        #region methods

        #region GET

        #region GetAll
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GovernorateDTO>>> GetAll(int? page)
        {
            int? pageSize = 10;
            if (page < 1 || pageSize < 1)
                return BadRequest(AppConstants.Response<string>(AppConstants.badRequestCode, AppConstants.invalidMessage));

            int governoratesCount = _governorateRepository.GetAll().Result.Count();
            if (governoratesCount == 0)
                return Ok(AppConstants.Response<string>(AppConstants.noContentCode, AppConstants.notContentMessage));

            IEnumerable<Governorate> governorates = await _governorateRepository.GetAllWithPagination(page: page ?? 1, pageSize: pageSize ?? governoratesCount);

            int totalPages = (int)Math.Ceiling((double)governoratesCount / pageSize ?? governoratesCount);
            if (totalPages < page)
                return BadRequest(AppConstants.Response<string>(AppConstants.badRequestCode, AppConstants.invalidMessage));

            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.getSuccessMessage, page ?? 1, totalPages, governoratesCount, governorates));
        }

        #region V4
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<GovernorateDTO>>> GetAll()
        //{
        //    var governorates = await _governorateRepository.GetAllGovernorates()
        //        .ProjectTo<GovernorateDTO>(_mapper.ConfigurationProvider)
        //        .ToListAsync();

        //    if (!governorates.Any())
        //    {
        //        return NotFound(AppConstants.GetEmptyList());
        //    }

        //    return Ok(governorates);
        //}
        #endregion

        #endregion

        #region GetById
        [HttpGet("{id}")]
        public async Task<ActionResult<Governorate>> GetById(int id)
        {
            Governorate governorate = await _governorateRepository.GetById(id);
            if (governorate == null)
                return NotFound(AppConstants.Response<string>(AppConstants.notFoundCode, AppConstants.notFoundMessage));

            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.getSuccessMessage, 1, 1, 1, governorate));
        }
        #endregion

        #endregion

        #region Add
        [HttpPost]
        public async Task<ActionResult> Add(GovernorateDTO governorateDTO)
        {
            Governorate? governorate = _mapper.Map<Governorate>(governorateDTO);
            await _governorateRepository.Add(governorate);

            return Created("", AppConstants.Response<object>(AppConstants.successCode, AppConstants.addSuccessMessage, 1, 1, 1, governorate));
        }
        #endregion

        #region Put
        [HttpPut("{id}")]
        public async Task<ActionResult> update(int id, GovernorateDTO governorateDTO)
        {
            if (await _governorateRepository.GetById(id) == null)
                return NotFound(AppConstants.Response<string>(AppConstants.notFoundCode, AppConstants.notFoundMessage));

            Governorate? governorate = _mapper.Map<Governorate>(governorateDTO);
            try
            {
                await _governorateRepository.Update(id, governorate);
            }
            catch (DbUpdateConcurrencyException e)
            {
                return Problem(statusCode: AppConstants.errorCode, title: AppConstants.errorMessage);
            }

            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.updateSuccessMessage, 1, 1, 1, governorate));
        }
        #endregion

        #region delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> delete(int id)
        {
            Governorate? governrate = await _governorateRepository.GetById(id);

            if (governrate == null)
                return NotFound(AppConstants.Response<string>(AppConstants.notFoundCode, AppConstants.notFoundMessage));

            try
            {
                await _governorateRepository.DeleteById(id);
                return Ok(AppConstants.Response<string>(AppConstants.successCode, AppConstants.deleteSuccessMessage));

            }
            catch (Exception e)
            {
                return Problem(statusCode: 500, title: e.Message);
            }
        }
        #endregion
        #endregion
    }
}
