using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OlxDataAccess.Governorates.Repositories;
using OlxDataAccess.Models;
using APIApp.AppContsants;
using APIApp.DTOs.GovernorateDTOs;
using Microsoft.EntityFrameworkCore;

namespace APIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GovernorateController : ControllerBase
    {
        #region fileds
        private IGovernorateRepository _governorateRepository;
        private IMapper _mapper;
        #endregion

        #region ctor
        public GovernorateController(IGovernorateRepository governorateRepository, IMapper mapper)
        {
            _governorateRepository = governorateRepository;
            _mapper = mapper;
        }
        #endregion

        #region methods

        #region GET
        #region GetAll
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Governorate>>> GetAll()
        {
            IEnumerable<Governorate> AllGovernorates = await _governorateRepository.GetAll();
            if (AllGovernorates.Count() == 0) { return NotFound(AppConstants.GetEmptyList()); }
            return Ok(AllGovernorates);
        }
        #endregion
        #region GetById
        [HttpGet("{id}")]
        public async Task<ActionResult<Governorate>> GetById(int id)
        {
            Governorate governorate = await _governorateRepository.GetById(id);
            if (governorate == null)
            {
                return NotFound(AppConstants.GetNotFound());
            }
            return Ok(governorate);
        }
        #endregion
        #endregion

        #region post
        [HttpPost]
        public async Task<ActionResult> Add(GovernorateDTO governorateDTO)
        {
            var gover = _mapper.Map<Governorate>(governorateDTO);
            await _governorateRepository.Add(gover);
            return Created("", gover);

        }
        #endregion

        #region Put
        [HttpPut("{id}")]
        public async Task<ActionResult> update(int id, GovernorateDTO governorateDTO)
        {
            if (await _governorateRepository.GetById(id) == null)
            {
                return NotFound(AppConstants.GetNotFound());
            }
            else
            {
                try
                {

                    var gover = _mapper.Map<Governorate>(governorateDTO);
                    await _governorateRepository.Update(id, gover);
                }
                catch (DbUpdateConcurrencyException e)
                {
                    return Problem(statusCode: 500, title: e.Message);
                }

                return Ok(AppConstants.UpdatedSuccessfully());
            }

        }
        #endregion

        #region delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> delete(int id)
        {
            Governorate? gover = await _governorateRepository.GetById(id);

            if (gover == null) return NotFound(AppConstants.GetNotFound());
            try
            {
                await _governorateRepository.DeleteById(id);
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
