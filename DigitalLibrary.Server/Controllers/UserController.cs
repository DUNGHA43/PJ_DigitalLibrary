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
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        private readonly TokenService _jwt;

        public UserController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
            _jwt = new TokenService(_config);
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
            await _userService.UpdateuserAsync(user);

            return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
        }

        [HttpPost("refresh-token")]
        [Authorize]
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
            await _userService.UpdateuserAsync(user);

            return Ok(new { AccessToken = newAccessToken, RefreshToken = newRefreshToken });
        }

        [HttpGet]
        [Authorize(Roles = "admin, stafflv1, stafflv2")]
        public async Task<IActionResult> GetAllAccount()
        {
            var accounts = await _userService.GetAllUsersAsync();
            return Ok(accounts);
        }

        [HttpPost("AddUser")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddUserAsync([FromBody] UsersDTO userDTO)
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
                status = true,
                refreshtoken = null,
                refreshtokenexpirytime = null
            };

            await _userService.AdduserAsync(user);
            return Ok(new { message = "Add user success!" });
        }

        [HttpPut("EditUser")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditUserAsync([FromBody] UsersDTO usersDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _userService.GetByUsernameAsync(usersDTO.username!);
            if (existingUser == null)
            {
                return NotFound($"user with {usersDTO.username} not found!");
            }

            existingUser.username = usersDTO.username ?? existingUser.username;
            existingUser.password = usersDTO.password ?? existingUser.password;
            existingUser.email = usersDTO.email ?? existingUser.email;
            existingUser.fullname = usersDTO.fullname ?? existingUser.fullname;
            existingUser.gender = usersDTO.gender ?? existingUser.gender;
            existingUser.birthday = usersDTO.birthday ?? existingUser.birthday;
            existingUser.phonenumber = usersDTO.phonenumber ?? existingUser.phonenumber;
            existingUser.identification = usersDTO.identification ?? existingUser.identification;
            existingUser.address = usersDTO.address ?? existingUser.address;
            existingUser.status = usersDTO.status;

            await _userService.UpdateuserAsync(existingUser);
            return Ok(new { message = "Edit user success!" });
        }

        [HttpDelete("DeleteUser")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUserAsync(string email)
        {
            var existingUser = await _userService.GetByEmailAsync(email);
            if (existingUser == null)
            {
                return NotFound($"User with email {email} not found!");
            }

            await _userService.DeleteuserAsync(email);
            return Ok(new { message = "Delete Success!"});
        }
    }
}
