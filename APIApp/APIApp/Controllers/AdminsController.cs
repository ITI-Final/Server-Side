using APIApp.AppContsants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OlxDataAccess.Admins.Repository;
using OlxDataAccess.Models;

namespace APIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        #region Fileds
        readonly IAdminRepository _adminRepository;
        #endregion

        #region Constructors
        public AdminsController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }
        #endregion

        #region Methods

        #region Get

        #region Get All
        // GET: api/Admins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll()
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
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdminById(int id)
        {
            if (await _adminRepository.GetAll() == null) return NotFound(AppConstants.GetEmptyList());

            Admin? admin = await _adminRepository.GetById(id);

            if (admin == null) return NotFound(AppConstants.GetNotFount());

            return admin;
        }
        #endregion

        #endregion

        #region Add
        // POST: api/Admin
        [HttpPost]
        public async Task<ActionResult<Admin>> PostCategory(Admin admin)
        {
            if (await _adminRepository.GetAll() == null) return NotFound(AppConstants.GetNotFount());

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

            if (admin == null) return NotFound(AppConstants.GetNotFount());
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
