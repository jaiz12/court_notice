using API.Services;
using BAL.Services.Master.BoardsService;
using DTO.Models.Auth;
using DTO.Models.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.MasterController
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BoardsController : ControllerBase
    {
        private readonly IBoardsService _IBoardsService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;
        public BoardsController
        (
            IBoardsService IBoardsService,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IAuthoriseRoles authoriseRoles
        )
        {
            _IBoardsService = IBoardsService;
            _userManager = userManager;
            _roleManager = roleManager;
            _authoriseRoles = authoriseRoles;
        }

        [HttpGet("GetBoards/{userId}/{companyName}")]
        public async Task<IActionResult> GetBoards(string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin','Employee', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var boards = await _IBoardsService.GetBoards();
            return Ok(new { result = boards });
        }

        [HttpPost("PostBoards/{userId}/{companyName}")]
        public async Task<IActionResult> Post(Boards_DTO data, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin', 'Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var boards = await _IBoardsService.Post(data);
            return Ok(new { result = boards });
        }



        [HttpPut("UpdateBoards/{userId}/{companyName}")]
        public async Task<IActionResult> Update(Boards_DTO id, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin', 'Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }
            var boards = await _IBoardsService.Update(id);
            return Ok(new { result = boards });
        }

        [HttpDelete("DeleteBoards/{userId}/{companyName}/{id}")]
        public async Task<IActionResult> Delete(long id, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin', 'Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var boards = await _IBoardsService.Delete(id);
            return Ok(new { result = boards });
        }

    }
}

