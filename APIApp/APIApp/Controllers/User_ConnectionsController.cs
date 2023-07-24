using Microsoft.AspNetCore.Authorization;

namespace APIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class User_ConnectionsController : ControllerBase
    {
        #region Fileds
        readonly IUserConnectionRepository _userConnectionRepository;
        #endregion

        #region Constructors
        public User_ConnectionsController(IUserConnectionRepository userConnectionRepository)
        {
            _userConnectionRepository = userConnectionRepository;
        }
        #endregion

        #region Methods
        #region Get
        [Authorize(Roles = "Admin")]
        [HttpGet]

        public async Task<ActionResult<IEnumerable<User_Connection>>> GetAllUsers(int? page)
        {
            int? pageSize = 10;
            if (page < 1 || pageSize < 1)
                return BadRequest(AppConstants.Response<string>(AppConstants.badRequestCode, AppConstants.invalidMessage));

            int usersCount = _userConnectionRepository.GetAll().Result.Count();
            if (usersCount == 0)
                return Ok(AppConstants.Response<string>(AppConstants.noContentCode, AppConstants.notContentMessage));

            IQueryable<User_Connection> user_Connection = await _userConnectionRepository.GetAllWithPagination(page: page ?? 1, pageSize: pageSize ?? usersCount);

            int totalPages = (int)Math.Ceiling((double)usersCount / pageSize ?? usersCount);
            if (totalPages < page)
                return BadRequest(AppConstants.Response<string>(AppConstants.badRequestCode, AppConstants.invalidMessage));

            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.getSuccessMessage, page ?? 1, totalPages, usersCount, user_Connection));

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            if (await _userConnectionRepository.GetAll() == null)
                return Ok(AppConstants.Response<string>(AppConstants.noContentCode, AppConstants.notContentMessage));

            User_Connection? user_Connection = await _userConnectionRepository.GetById(id);

            if (user_Connection == null)
                return Ok(AppConstants.Response<string>(AppConstants.notFoundCode, AppConstants.notFoundMessage));

            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.getSuccessMessage, 1, 1, 1, user_Connection));
        }
        #endregion

        #endregion

    }
}
