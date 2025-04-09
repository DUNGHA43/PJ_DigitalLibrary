using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;
using DigitalLibrary.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLibrary.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPermissionController : Controller
    {
        private readonly IUserPermissionServices _userPermissionService;

        public UserPermissionController(IUserPermissionServices userPermissionService)
        {
            _userPermissionService = userPermissionService;
        }

        [HttpGet("getall-noqueries")]
        [Authorize]
        public async Task<IActionResult> GetAllUserPermission()
        {
            var result = await _userPermissionService.GetAllUserPermission();
            if (result == null)
            {
                return NotFound("No data!");
            }

            return Ok(result);
        }

        [HttpPost("adduserpermission")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddUserPermission([FromBody] UserPermissionsDTO userPermissions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userPermission = new UserPermissions()
            {
                userid = userPermissions.userid,
                canread = userPermissions.canread,
                candowload = userPermissions.candowload
            };

            await _userPermissionService.AddUserPermissionAsync(userPermission);

            return Ok(new { data = userPermission, message = "Add success!" });
        }

        [HttpPut("updateuserpermission")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateUserPermission([FromBody] UserPermissionsDTO userPermissionsDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userPermission = await _userPermissionService.GetUserPermissionByIdAsync(userPermissionsDTO.userid);
            if (userPermission == null)
            {
                return NotFound($"Category with id {userPermission!.userid} not found!");
            }

            userPermission.canread = userPermissionsDTO.canread;
            userPermission.candowload = userPermissionsDTO.candowload;

            await _userPermissionService.UpdateUserPermissionAsync(userPermission);
            return Ok(new { data = userPermission, message = "Update success!" });
        }
    }
}
