namespace APIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldsController : ControllerBase
    {
        private readonly IFieldRepository _fieldRepository;
        private readonly IMapper _mapper;

        public FieldsController(IFieldRepository fieldRepository, IMapper mapper)
        {
            _fieldRepository = fieldRepository;
            _mapper = mapper;
        }

        #region get
        #region GETbyId
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(await _fieldRepository.GetById(id));
        }
        #endregion
        #endregion

        [HttpPut("{id}")]
        public async Task<ActionResult> update(FieldPostDTO field, int id)
        {
            if (id != field.Id)
            {
                return NotFound();
            }
            var fieldMap = _mapper.Map<Field>(field);
            await _fieldRepository.Update(id, fieldMap);
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult> addfield(FieldPostDTO field)
        {
            if (field != null)
            {
                var fieldMap = _mapper.Map<Field>(field);
                await _fieldRepository.Add(fieldMap);
                return NoContent();

            }
            return BadRequest();

        }
    }
}
