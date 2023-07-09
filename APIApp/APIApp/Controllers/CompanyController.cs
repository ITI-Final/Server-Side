namespace APIApp.Controllers
{
    using OlxDataAccess.Companies.Repositories;

    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        protected readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        public CompanyController(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetAllCompanies()
        {
            return Ok(await _companyRepository.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> getCatById(int id)
        {

            return Ok(await _companyRepository.GetById(id));

        }

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

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, CompanyDTO companyDTO)
        {

            if (id != companyDTO.Id)
            {
                return NotFound();
            }

            var company = _mapper.Map<Company>(companyDTO);

            await _companyRepository.Update(id, company);
            return Ok(AppConstants.UpdatedSuccessfully());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCompany(int id)
        {
            await _companyRepository.DeleteById(id);
            return Ok(AppConstants.DeleteSuccessfully());

        }
    }
}
