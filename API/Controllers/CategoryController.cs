using BAL.Services.Category;
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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("PostCategory")]
        public async Task<IActionResult> Post(CategoryDTO categoryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var result = await _categoryService.Post(categoryDTO);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("PutCategory")]
        public async Task<IActionResult> Put(CategoryDTO categoryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _categoryService.Put(categoryDTO);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetCategory")]
        public async Task<IActionResult> Get()
        {
            
            try
            {
                var result = await _categoryService.Get();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("DeleteCategory/{category_id}")]
        public async Task<IActionResult> Delete(long category_id)
        {
           
            try
            {
                var result = await _categoryService.Delete(category_id);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
