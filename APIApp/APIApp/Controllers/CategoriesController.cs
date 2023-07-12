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


        #region Methods

        #endregion
        // GET: api/Categories
        #region get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories(int? page = null, int? pageSize = null)
        {
            if (page < 1 || pageSize < 1)
                return BadRequest(AppConstants.Response<string>(AppConstants.badRequestCode, AppConstants.invalidMessage));

            int CategoriesCount = _categoryRepository.GetAll().Result.Count();
            if (CategoriesCount == 0)
                return Ok(AppConstants.Response<string>(AppConstants.noContentCode, AppConstants.notContentMessage));

            IEnumerable<Category> categories = await _categoryRepository.GetAllWithPagination(page: page ?? 1, pageSize: pageSize ?? CategoriesCount);

            int totalPages = (int)Math.Ceiling((double)CategoriesCount / pageSize ?? CategoriesCount);
            if (totalPages < page)
                return BadRequest(AppConstants.Response<string>(AppConstants.badRequestCode, AppConstants.invalidMessage));

            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.getSuccessMessage, categories));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            if (await _categoryRepository.GetAll() == null)
                return Ok(AppConstants.Response<string>(AppConstants.noContentCode, AppConstants.notContentMessage));

            Category? category = await _categoryRepository.GetById(id);

            if (category == null)
                return NotFound(AppConstants.Response<string>(AppConstants.notFoundCode, AppConstants.notFoundMessage));

            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.getSuccessMessage, category));
        }


        [HttpGet]
        [Route("names")]
        public async Task<ActionResult> GetCategoriesNames()
        {
            IEnumerable<Category> category = await _categoryRepository.GetAll();
            if (category.Count() == 0)
                NotFound(AppConstants.Response<string>(AppConstants.notFoundCode, AppConstants.notFoundMessage));

            List<GetCatNameDTO> getCategortNameDTOs = new List<GetCatNameDTO>();
            foreach (Category item in category)
            {
                GetCatNameDTO names = new GetCatNameDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Label = item.Label,
                    Label_Ar = item.Label_Ar,
                };
                getCategortNameDTOs.Add(names);
            }

            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.getSuccessMessage, getCategortNameDTOs));
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult> AddCatrgories(CategoryPostDTO category)
        {
            if (category == null)
                //return BadRequest();
                return BadRequest(AppConstants.Response<string>(AppConstants.badRequestCode, AppConstants.invalidMessage));

            #region DTOPost
            //List<Choice> choicelist = new List<Choice>();

            //List<Field> fieldList = new List<Field>();
            //foreach (var item in category.Fields)
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
            //    Id = category.Id,
            //    Name = category.Name,
            //    Slug = category.Slug,
            //    Parent_Id = category.Parent_Id,
            //    Description = category.Description,
            //    Tags = category.Tags,
            //    Created_Date = category.Created_Date,
            //    Label = category.Label,
            //    Label_Ar = category.Label_Ar,
            //    Admin_Id = category.Admin_Id,
            //    Fields = fieldList

            //};
            #endregion

            #region autoMapper
            Category? categories = _mapper.Map<Category>(category);
            #endregion
            await _categoryRepository.Add(categories);
            //return Created("", categories);
            return Created("GetCategories", AppConstants.Response<object>(AppConstants.successCode, AppConstants.addSuccessMessage, categories));
        }

        [HttpPost]
        [Route("main")]
        public async Task<ActionResult> AddmainCategory(addMainCategoryDTO main)
        {
            var Main = _mapper.Map<Category>(main);
            await _categoryRepository.Add(Main);

            //return NoContent();
            return Created("GetCategories", AppConstants.Response<object>(AppConstants.successCode, AppConstants.addSuccessMessage, Main));
        }
        #endregion

        #region Update

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, CategoryPostDTO category)
        {
            if (id != category.Id)
                //return NotFound();
                return NotFound(AppConstants.Response<string>(AppConstants.notFoundCode, AppConstants.notFoundMessage));



            Category? category1 = _mapper.Map<Category>(category);
            await _categoryRepository.Update(id, category1);
            //return NoContent();
            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.updateSuccessMessage, category1));
        }
        #endregion

        #region delete

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            await _categoryRepository.DeleteById(id);
            return Ok(AppConstants.Response<string>(AppConstants.successCode, AppConstants.deleteSuccessMessage));
        }
        #endregion

    }
}
