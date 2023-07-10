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
        public async Task<ActionResult<IEnumerable<GovernorateDTO>>> GetAll()
        {
            List<GovernorateDTO> Governorates = new List<GovernorateDTO>();
            List<CitiesDTO> CitiesDTO = new List<CitiesDTO>();

            IEnumerable<Governorate> AllGovernorates = await _governorateRepository.GetAll();

            foreach (Governorate governorate in AllGovernorates)
            {

                foreach (City city in governorate.Cities)
                {
                    CitiesDTO cityDTO = new CitiesDTO()
                    {
                        Id = city.Id,
                        Governorate_Id = city.Governorate_Id,
                        City_Name_Ar = city.City_Name_Ar,
                        City_Name_En = city.City_Name_En,
                    };
                    CitiesDTO.Add(cityDTO);
                }

                GovernorateDTO governorateDTO = new GovernorateDTO()
                {
                    Id = governorate.Id,
                    Governorate_Name_Ar = governorate.Governorate_Name_Ar,
                    Governorate_Name_En = governorate.Governorate_Name_En,
                    cities = CitiesDTO,
                };
                Governorates.Add(governorateDTO);
            }

            if (AllGovernorates.Count() == 0)
                return Ok(AppConstants.Response<string>(AppConstants.noContentCode, AppConstants.notContentMessage));

            #region V2
            //    var governorates = await _governorateRepository.GetAll()
            //.Select(g => new GovernorateDTO
            //{
            //    Id = g.Id,
            //    Governorate_Name_Ar = g.Governorate_Name_Ar,
            //    Governorate_Name_En = g.Governorate_Name_En,
            //    cities = g.Cities.Select(c => new CitiesDTO
            //    {
            //        Id = c.Id,
            //        Governorate_Id = c.Governorate_Id,
            //        City_Name_Ar = c.City_Name_Ar,
            //        City_Name_En = c.City_Name_En
            //    }).ToList()
            //}).ToListAsync();

            //    if (!governorates.Any())
            //    {
            //        return NotFound(AppConstants.GetEmptyList());
            //    }
            #endregion

            #region V3

            //  var governorates = _governorateRepository.GetAllGovernorates();


            //  governorates.ProjectTo<GovernorateDTO>(_mapper.ConfigurationProvider)
            //.ToListAsync();

            //  if (!governorates.Any())
            //  {
            //      return NotFound(AppConstants.GetEmptyList());
            //  }

            #endregion

            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.getSuccessMessage, AllGovernorates));
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

            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.getSuccessMessage, governorate));
        }
        #endregion

        #endregion

        #region Add
        [HttpPost]
        public async Task<ActionResult> Add(GovernorateDTO governorateDTO)
        {
            Governorate? governorate = _mapper.Map<Governorate>(governorateDTO);
            await _governorateRepository.Add(governorate);

            return Created("", AppConstants.Response<object>(AppConstants.successCode, AppConstants.addSuccessMessage, governorate));
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

            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.updateSuccessMessage, governorate));
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
