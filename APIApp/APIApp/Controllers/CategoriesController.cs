
namespace APIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoriesController : ControllerBase
    {
        protected readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;

        }
        // GET: api/Categories
        #region get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return Ok(await _categoryRepository.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> getCatById(int id)
        {
            Category category = await _categoryRepository.GetById(id);
            return Ok(category);

        }
        [HttpGet]
        [Route("Names")]
        public async Task<ActionResult> getCatNames()
        {
            IEnumerable<Category> cat = await _categoryRepository.GetAll();
            if (cat.Count() == 0) return NotFound(AppConstants.GetEmptyList());
            List<GetCatNameDTO> getCatNameDTOs = new List<GetCatNameDTO>();
            foreach (var item in cat)
            {

                GetCatNameDTO names = new GetCatNameDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Label = item.Label,
                    Label_Ar = item.Label_Ar,
                };
                getCatNameDTOs.Add(names);
            }
            return Ok(getCatNameDTOs);


        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult> AddCatrgories(CategoryPostDTO cat)
        {
            if (cat == null)
            {

                return BadRequest();
            }
            #region DTOPost
            //List<Choice> choicelist = new List<Choice>();

            //List<Field> fieldList = new List<Field>();
            //foreach (var item in cat.Fields)
            //{
            //    foreach (var i in item.Choices)
            //    {
            //        Choice choice = new Choice()
            //        {
            //            Id = i.Id,
            //            Field_Id = i.Field_Id,
            //            Label = i.Label,
            //            Label_Ar = i.Label_Ar,
            //            Slug = i.Slug,
            //            Icon = i.Icon,
            //        };
            //        choicelist.Add(choice);
            //    }
            //    Field field = new Field()
            //    {
            //        Id = item.Id,
            //        Name = item.Name,
            //        Label = item.Label,
            //        Label_Ar = item.Label_Ar,
            //        Value_Type = item.Value_Type,
            //        Choice_Type = item.Choice_Type,
            //        Max_Length = item.Max_Length,
            //        Min_Length = item.Min_Length,
            //        Max_Value = item.Max_Value,
            //        Min_Value = item.Min_Value,
            //        Is_Required = item.Is_Required,
            //        Parent_Id = item.Parent_Id,
            //        Choices = choicelist

            //    };
            //    fieldList.Add(field);
            //}
            //Category c = new Category()
            //{
            //    Id = cat.Id,
            //    Name = cat.Name,
            //    Slug = cat.Slug,
            //    Parent_Id = cat.Parent_Id,
            //    Description = cat.Description,
            //    Tags = cat.Tags,
            //    Created_Date = cat.Created_Date,
            //    Label = cat.Label,
            //    Label_Ar = cat.Label_Ar,
            //    Admin_Id = cat.Admin_Id,
            //    Fields = fieldList

            //};
            #endregion

            #region autoMapper
            var categories = _mapper.Map<Category>(cat);
            #endregion
            await _categoryRepository.Add(categories);
            return Created("", categories);

        }
        [HttpPost]
        [Route("main")]
        public async Task<ActionResult> AddmainCategory(addMainCategoryDTO main)
        {
            var Main = _mapper.Map<Category>(main);
            await _categoryRepository.Add(Main);
            return NoContent();
        }
        #endregion

        #region Update

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, CategoryPostDTO cat)
        {

            if (id != cat.Id)
            {
                return NotFound();
            }

            var category = _mapper.Map<Category>(cat);
            await _categoryRepository.Update(id, category);
            return NoContent();
        }
        #endregion

        #region delete

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            await _categoryRepository.DeleteById(id);
            return Ok();
        }
        #endregion

    }
}
