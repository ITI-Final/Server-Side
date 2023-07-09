using APIApp.DTOs.GovernorateDTOs;
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

            if (AllGovernorates.Count() == 0) return NotFound(AppConstants.GetEmptyList());

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

            return Ok(AllGovernorates);
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
            {
                return NotFound(AppConstants.GetNotFound());
            }
            return Ok(governorate);
        }
        #endregion
        #endregion

        #region post
        [HttpPost]
        public async Task<ActionResult> Add(GovernorateDTO governorateDTO)
        {
            var gover = _mapper.Map<Governorate>(governorateDTO);
            await _governorateRepository.Add(gover);
            return Created("", gover);

        }
        #endregion

        #region Put
        [HttpPut("{id}")]
        public async Task<ActionResult> update(int id, GovernorateDTO governorateDTO)
        {
            if (await _governorateRepository.GetById(id) == null)
            {
                return NotFound(AppConstants.GetNotFound());
            }
            else
            {
                try
                {

                    var gover = _mapper.Map<Governorate>(governorateDTO);
                    await _governorateRepository.Update(id, gover);
                }
                catch (DbUpdateConcurrencyException e)
                {
                    return Problem(statusCode: 500, title: e.Message);
                }

                return Ok(AppConstants.UpdatedSuccessfully());
            }

        }
        #endregion

        #region delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> delete(int id)
        {
            Governorate? gover = await _governorateRepository.GetById(id);

            if (gover == null) return NotFound(AppConstants.GetNotFound());
            try
            {
                await _governorateRepository.DeleteById(id);
                return Ok(AppConstants.DeleteSuccessfully());
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
