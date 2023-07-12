namespace APIApp.Controllers
{
    using OlxDataAccess.Companies.Repositories;
    using OlxDataAccess.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        #region Fields
        protected readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public CompaniesController(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;

        }

        #endregion

        #region Methods

        #region Get

        #region Get All
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetAllCompanies(int? page = null, int? pageSize = null)
        {
            if (page < 1 || pageSize < 1)
                return BadRequest(AppConstants.Response<string>(AppConstants.badRequestCode, AppConstants.invalidMessage));

            int companiesCount = _companyRepository.GetAll().Result.Count();
            if (companiesCount == 0)
                return Ok(AppConstants.Response<string>(AppConstants.noContentCode, AppConstants.notContentMessage));

            IEnumerable<Company> company = await _companyRepository.GetAllWithPagination(page: page ?? 1, pageSize: pageSize ?? companiesCount);

            int totalPages = (int)Math.Ceiling((double)companiesCount / pageSize ?? companiesCount);
            if (totalPages < page)
                return BadRequest(AppConstants.Response<string>(AppConstants.badRequestCode, AppConstants.invalidMessage));

            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.getSuccessMessage, company));
        }
        #endregion

        #region Get By ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> getCatById(int id)
        {
            if (await _companyRepository.GetAll() == null)
                return Ok(AppConstants.Response<string>(AppConstants.noContentCode, AppConstants.notContentMessage));

            Company? company = await _companyRepository.GetById(id);

            if (company == null)
                return NotFound(AppConstants.Response<string>(AppConstants.notFoundCode, AppConstants.notFoundMessage));

            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.getSuccessMessage, company));
        }
        #endregion

        #endregion

        #region Add

        [HttpPost]
        public async Task<ActionResult> AddCompanies(CompanyDTO companyDTO)
        {
            if (companyDTO == null)
            {
                return BadRequest();
            }
            var company = _mapper.Map<Company>(companyDTO);
            await _companyRepository.Add(company);
            return Ok();


        }
        #endregion

        #region Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, CompanyDTO companyDTO)
        {

            if (id != companyDTO.Id)
            {
                return NotFound();
            }

            Company? company = _mapper.Map<Company>(companyDTO);

            await _companyRepository.Update(id, company);
            //return Ok(AppConstants.UpdatedSuccessfully());
            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.updateSuccessMessage, company));
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCompany(int id)
        {
            await _companyRepository.DeleteById(id);
            //return Ok(AppConstants.DeleteSuccessfully());
            return Ok(AppConstants.Response<string>(AppConstants.successCode, AppConstants.deleteSuccessMessage));


        }
        #endregion

        #endregion





    }
}
