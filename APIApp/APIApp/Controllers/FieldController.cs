

namespace APIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldController : ControllerBase
    {
        private readonly IFieldRepository _fieldRepository;
        private readonly IMapper _mapper;

        public FieldController(IFieldRepository fieldRepository, IMapper mapper)
        {
            _fieldRepository = fieldRepository;
            _mapper = mapper;
        }

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
