
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
        #endregion

        #region Constructors
        public AdminsController(IJWT jWT, IConfiguration configuration, IAdminRepository adminRepository, IAuthentication<Admin> authentication)
        {
            _jwt = jWT;
            _configuration = configuration;
            _adminRepository = adminRepository;
            _authentication = authentication;
        }
        #endregion

        #region Methods

        #region Authentication
        #region Login
        // api/Admin/Login
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromForm] string email, [FromForm] string password)
        {

            #region Check Parameters 
            if (email == null || password == null) return BadRequest(AppConstants.GetBadRequest());
            #endregion

            Admin? admin = await _authentication.Login(email, password);

            #region Check is Existed

            if (admin == null)
                return BadRequest(AppConstants.GetBadRequest());
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

            object response = AppConstants.AdminLoginSuccessfully(adminLoginDTO, _jwt.GenentateToken(claims, numberOfDays: 1));

            #endregion


            return Ok(response);
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
            if (admins.Count() == 0) return NotFound(AppConstants.GetEmptyList());

            return Ok(admins);
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
            if (await _adminRepository.GetAll() == null) return NotFound(AppConstants.GetEmptyList());

            Admin? admin = await _adminRepository.GetById(id);

            if (admin == null) return NotFound(AppConstants.GetNotFound());

            return admin;
        }
        #endregion

        #endregion

        #region Add
        // POST: api/Admin
        [HttpPost]
        public async Task<ActionResult<Admin>> PostCategory(Admin admin)
        {
            if (await _authentication.IsEmailTakenAsync(admin.Email)) return BadRequest(AppConstants.GetEmailFound());
            if (await _adminRepository.GetAll() == null) return NotFound(AppConstants.GetNotFound());

            await _adminRepository.Add(admin);

            return CreatedAtAction("GetAll", new { id = admin.Id }, admin);
        }
        #endregion

        #region Update
        // PUT: api/Admin/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Admin admin)
        {
            if (id != admin.Id) return BadRequest(AppConstants.GetBadRequest());

            try
            {
                await _adminRepository.Update(id, admin);
            }
            catch (DbUpdateConcurrencyException e)
            {
                return Problem(statusCode: 500, title: e.Message);
            }

            return Ok(AppConstants.UpdatedSuccessfully());
        }

        #endregion

        #region Delete
        // DELETE: api/Admin/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            Admin? admin = await _adminRepository.GetById(id);

            if (admin == null) return NotFound(AppConstants.GetNotFound());
            try
            {
                await _adminRepository.DeleteById(id);
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
