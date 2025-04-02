using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;
using DigitalLibrary.Server.Services.Service;
using DigitalLibrary.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigitalLibrary.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserServices _userService;
        private readonly IUserPermissionServices _userPermissionServices;
        private readonly IConfiguration _config;
        private readonly TokenService _jwt;
        private readonly IWebHostEnvironment _env;
        public UserController(IUserServices userService, IUserPermissionServices userPermissionServices, IConfiguration config, IWebHostEnvironment env)
        {
            _userService = userService;
            _userPermissionServices = userPermissionServices;
            _config = config;
            _jwt = new TokenService(_config);
            _env = env;
        }

        [HttpPost("login")]
        public async Task<IActionResult>Login([FromBody] Login loginrequest) 
        {
            var user = _userService.ValidateUser(loginrequest.username, loginrequest.password);

            if( user == null)
            {
                return Unauthorized("Wrong username or password!");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                new Claim(ClaimTypes.Name, user.username),
                new Claim(ClaimTypes.Role, user.role.rolenameen)
            };

            var accessToken = _jwt.GenerateAccessToken(claims);
            var refreshToken = _jwt.GenerateRefreshToken();

            user.refreshtoken = refreshToken;
            user.refreshtokenexpirytime = DateTime.UtcNow.AddDays(7);
            await _userService.UpdateUserAsync(user);

            return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var user = await _userService.GetUserByRefreshTokenAsync(request.RefreshToken);

            if (user == null || user.refreshtokenexpirytime < DateTime.UtcNow)
            {
                return Unauthorized("Refresh Token is invalid or expired!!");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                new Claim(ClaimTypes.Name, user.username),
                new Claim(ClaimTypes.Role, user.role.rolenameen)
            };

            var newAccessToken = _jwt.GenerateAccessToken(claims);
            var newRefreshToken = _jwt.GenerateRefreshToken();

            user.refreshtoken = newRefreshToken;
            user.refreshtokenexpirytime = DateTime.UtcNow.AddDays(7);
            await _userService.UpdateUserAsync(user);

            return Ok(new { AccessToken = newAccessToken, RefreshToken = newRefreshToken });
        }

        [HttpGet("getall-noqueries")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllAccount()
        {
            var accounts = await _userService.GetAllUsersAsync();
            return Ok(accounts);
        }

        [HttpGet("getall")]
        [Authorize(Roles = "admin, stafflv1, stafflv2")]
        public async Task<IActionResult> GetAllAuthorsAsync([FromQuery] int pageNumber = 1, int pageSize = 10, string searchName = "", string searchEmail = "", int? searchRole = null, bool? searchStatus = null)
        {
            var (user, totalCount) = await _userService.GetAllUsersAsync(pageNumber, pageSize, searchName, searchEmail, searchRole, searchStatus);

            if (user == null || !user.Any())
            {
                return NotFound(new { message = "Không tìm thấy người dùng nào." });
            }

            var response = new
            {
                Data = user,
                TotalRecords = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };

            return Ok(response);
        }

        [HttpGet("getimg/{id}")]
        [Authorize]
        public async Task<IActionResult> GetDocumentImage(int id)
        {
            if (id <= 0)
                return BadRequest("ID tài liệu không hợp lệ!");

            var user = await _userService.FindUserByIdAsync(id);
            if (user == null || string.IsNullOrEmpty(user.imageurl))
            {
                return NotFound("Không tìm thấy người dùng hoặc người dùng không có ảnh.");
            }

            var imagePath = Path.Combine("wwwroot/UploadImages", user.imageurl);

            string fullPath = Path.Combine(_env.WebRootPath, imagePath.TrimStart('/').Replace("/", "\\"));

            if (!System.IO.File.Exists(fullPath))
            {
                return NotFound($"Ảnh không tồn tại trên server: {fullPath}");
            }

            try
            {
                var imageBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
                return File(imageBytes, "image/jpeg");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi khi tải ảnh.");
            }
        }

        [HttpPost("adduser")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddUserAsync([FromForm] UsersDTO userDTO, IFormFile? imgFile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new Users
            {
                roleid = userDTO.roleid,
                username = userDTO.username,
                password = userDTO.password,
                email = userDTO.email,
                fullname = userDTO.fullname,
                gender = userDTO.gender,
                birthday = userDTO.birthday,
                phonenumber = userDTO.phonenumber,
                identification = userDTO.identification,
                address = userDTO.address,
                createdate = DateTime.Now,
                status = userDTO.status,
                refreshtoken = null,
                refreshtokenexpirytime = null
            };

            await _userService.AdduserAsync(user, imgFile);

            var userPermission = new UserPermissions
            {
                userid = user.id,
                canread = true,
                candowload = false,
            };

            await _userPermissionServices.AddUserPermissionAsync(userPermission);

            return Ok(new { message = "Add user success!" });
        }

        [HttpPut("edituser")]
        [Authorize]
        public async Task<IActionResult> EditUserAsync([FromForm] UsersDTO userDTO, IFormFile? imgFile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _userService.GetByUsernameAsync(userDTO.username!);
            if (existingUser == null)
            {
                return NotFound($"user with {userDTO.username} not found!");
            }

            existingUser.username = userDTO.username ?? existingUser.username;
            existingUser.password = userDTO.password ?? existingUser.password;
            existingUser.email = userDTO.email ?? existingUser.email;
            existingUser.fullname = userDTO.fullname ?? existingUser.fullname;
            existingUser.gender = userDTO.gender;
            existingUser.birthday = userDTO.birthday ?? existingUser.birthday;
            existingUser.phonenumber = userDTO.phonenumber ?? existingUser.phonenumber;
            existingUser.identification = userDTO.identification ?? existingUser.identification;
            existingUser.address = userDTO.address ?? existingUser.address;
            existingUser.status = userDTO.status;

            await _userService.UpdateUserAsync(existingUser, imgFile);
            return Ok(new { message = "Edit user success!" });
        }

        [HttpDelete("deleteuser")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUserAsync([FromBody] int id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok(new { message = "Delete Success!"});
        }

        [HttpDelete("deletemulti-users")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteMultipleUsersAsync([FromBody] List<int> userIds)
        {
            await _userService.DeleteMultipleUsersAsync(userIds);
            return Ok(new { data = "Delete Success!" });
        }
    }
}
