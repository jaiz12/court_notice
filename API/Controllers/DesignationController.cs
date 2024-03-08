using BAL.Services.Designation;
using DTO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Land Registration,Marriage Registration, Inheriantance Registration, Case Hearing, Others")]
    // =============================================
    // -- Author:		Jaideep Roy
    // -- Create date: 23-Feb-2024
    // =============================================
    public class DesignationController : ControllerBase
    {
        private readonly IDesignationService _designationService;
        public DesignationController(IDesignationService designationService)
        {
            _designationService = designationService;
        }

        [HttpPost("PostDesignation")]
        public async Task<IActionResult> Post(DesignationDTO designationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var result = await _designationService.Post(designationDTO);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("PutDesignation")]
        public async Task<IActionResult> Put(DesignationDTO designationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _designationService.Put(designationDTO);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetDesignation")]
        public async Task<IActionResult> Get()
        {
            
            try
            {
                var result = await _designationService.Get();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("DeleteDesignation/{designation_id}")]
        public async Task<IActionResult> Delete(long designation_id)
        {
           
            try
            {
                var result = await _designationService.Delete(designation_id);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
