using BAL.Services.Department;
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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpPost("PostDepartment")]
        public async Task<IActionResult> Post(DepartmentDTO departmentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var result = await _departmentService.Post(departmentDTO);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("PutDepartment")]
        public async Task<IActionResult> Put(DepartmentDTO departmentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _departmentService.Put(departmentDTO);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetDepartment")]
        public async Task<IActionResult> Get()
        {
            
            try
            {
                var result = await _departmentService.Get();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("DeleteDepartment/{department_id}")]
        public async Task<IActionResult> Delete(long department_id)
        {
           
            try
            {
                var result = await _departmentService.Delete(department_id);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
