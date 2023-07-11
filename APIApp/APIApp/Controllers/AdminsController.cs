using APIApp.DTOs.Admin;

namespace APIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        #region Fileds
        readonly IJWT _jwt;
        readonly IConfiguration _configuration;
        readonly IAdminRepository _adminRepository;
        readonly IAuthentication<Admin> _authentication;
        readonly IMapper _mapper;
        #endregion

        #region Constructors
        public AdminsController(IJWT jWT, IConfiguration configuration, IAdminRepository adminRepository, IAuthentication<Admin> authentication, IMapper mapper)
        {
            _jwt = jWT;
            _configuration = configuration;
            _adminRepository = adminRepository;
            _authentication = authentication;
            _mapper = mapper;
        }
        #endregion

        #region Methods

        #region Authentication
        #region Login
        // api/Admin/Login
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromForm] string email, [FromForm] string password)
        {

            Admin? admin = await _authentication.Login(email, password);

            #region Check is Existed

            if (admin == null)
                return NotFound(AppConstants.Response<string>(AppConstants.notFoundCode, AppConstants.notFoundMessage));
            #endregion

            #region Define Claims
            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration[key: "Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(type: "name", admin.Name),
                new Claim(ClaimTypes.Role , "Admin"),
                new Claim(type: "Id", admin.Id.ToString()),
            };
            #endregion

            #region Response Formatter

            ICollection<Permission> permissions = admin.Permissions;
            List<PermissionDTO> permissionsDTO = new();

            foreach (Permission permission in permissions)
            {
                PermissionDTO permissionDTO = new PermissionDTO
                {
                    Id = permission.Id,
                    Section = permission.Section,
                    Can_Add = permission.Can_Add,
                    Can_Delete = permission.Can_Delete,
                    Can_Edit = permission.Can_Edit,
                    Can_View = permission.Can_View,
                };
                permissionsDTO.Add(permissionDTO);
            }

            AdminLoginDTO adminLoginDTO = new AdminLoginDTO()
            {
                Id = admin.Id,
                Name = admin.Name,
                Email = admin.Email,
                Permissions = permissionsDTO,
            };

            #endregion

            return Ok(AppConstants.LoginSuccessfully(adminLoginDTO, _jwt.GenentateToken(claims, numberOfDays: 1)));
        }
        #endregion

        #endregion

        #region Get

        #region Get All
        // GET: api/Admins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAll()
        {
            IEnumerable<Admin>? admins = await _adminRepository.GetAll();
            if (admins.Count() == 0)
                return Ok(AppConstants.Response<string>(AppConstants.noContentCode, AppConstants.notContentMessage));


            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.getSuccessMessage, admins));

        }

        //[HttpGet]
        //public async Task<HttpResponseMessage> GetHH()
        //{
        //    IEnumerable<Admin>? admins = await _adminRepository.GetAll();
        //    if (admins.Count() == 0)
        //    {
        //        HttpResponseMessage? response = new HttpResponseMessage(HttpStatusCode.NotFound);
        //        response.Content = new StringContent("admins not found");
        //        response.StatusCode = HttpStatusCode.NotFound;
        //        return response;
        //    }
        //    HttpResponseMessage? respo = new HttpResponseMessage(HttpStatusCode.OK);
        //    respo.Content = new ObjectContent<IEnumerable<Admin>>(admins, new JsonMediaTypeFormatter());

        //    return respo;

        //}

        #endregion

        #region Get By Id
        // GET: api/Admin/5
        //[Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdminById(int id)
        {
            if (await _adminRepository.GetAll() == null)
                return Ok(AppConstants.Response<string>(AppConstants.noContentCode, AppConstants.notContentMessage));

            Admin? admin = await _adminRepository.GetById(id);

            if (admin == null)
                return NotFound(AppConstants.Response<string>(AppConstants.notFoundCode, AppConstants.notFoundMessage));

            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.getSuccessMessage, admin));

        }
        #endregion

        #endregion

        #region Add
        // POST: api/Admin
        [HttpPost]
        public async Task<ActionResult<Admin>> Add(AdminDTO adminDto)
        {

            Admin admin = _mapper.Map<Admin>(adminDto);
            if (await _authentication.IsEmailTakenAsync(adminDto.Email))
                return BadRequest(AppConstants.Response<string>(AppConstants.badRequestCode, AppConstants.emailIsAlreadyMessage));

            if (await _adminRepository.GetAll() == null)
                return NotFound(AppConstants.Response<string>(AppConstants.notFoundCode, AppConstants.notFoundMessage));


            await _adminRepository.Add(admin);

            return Created("", AppConstants.Response<object>(AppConstants.successCode, AppConstants.addSuccessMessage, admin));
        }
        #endregion

        #region Update
        // PUT: api/Admin/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Admin admin)
        {
            if (id != admin.Id)
                return NotFound(AppConstants.Response<string>(AppConstants.notFoundCode, AppConstants.notFoundMessage));

            try
            {
                await _adminRepository.Update(id, admin);
            }
            catch (DbUpdateConcurrencyException e)
            {
                return Problem(statusCode: AppConstants.errorCode, title: AppConstants.errorMessage);
            }

            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.updateSuccessMessage, admin));
        }

        #endregion

        #region Delete
        // DELETE: api/Admin/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Admin? admin = await _adminRepository.GetById(id);

            if (admin == null)
                return NotFound(AppConstants.Response<string>(AppConstants.notFoundCode, AppConstants.notFoundMessage));

            try
            {
                await _adminRepository.DeleteById(id);
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
