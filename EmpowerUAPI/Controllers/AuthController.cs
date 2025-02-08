using EmpowerUAPI.Dtos;
using EmpowerUAPI.Helpers;
using EmpowerUAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmpowerUAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthHelper _authHelper;
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration, IAuthHelper authHelper, IUserService userService)
        {
            _configuration = configuration;
            _authHelper = authHelper;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto regUser)
        {
            UserRegistrationResult result = await _userService.RegisterUser(regUser);
            if(!result.IsSuccess)
            {
                return BadRequest(new { message = result.Message });
            }
            var claims = new List<Claim>();
            var expiresAt = DateTime.UtcNow.AddMinutes(5);

            return Ok(new
            {
                access_token = _authHelper.CreateToken(claims, expiresAt),
                expires_at = expiresAt,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUser)
        {
            bool isValid = await _userService.ValidateUserCredentials(loginUser);
            if(!isValid)
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }
            var claims = new List<Claim>();
            var expiresAt = DateTime.UtcNow.AddMinutes(5);

            return Ok(new
            {
                access_token = _authHelper.CreateToken(claims, expiresAt),
                expires_at = expiresAt,
            });
        }
    }
}
