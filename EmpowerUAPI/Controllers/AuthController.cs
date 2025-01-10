using EmpowerUAPI.Helpers;
using EmpowerUAPI.Models.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmpowerUAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] Credential credential)
        {
            // check credential against a db
            if(credential.UserName == "" && credential.Password == "")
            {
                AuthHelper authHelper = new AuthHelper(_configuration);
                var claims = new List<Claim>();

                var expiresAt = DateTime.UtcNow.AddMinutes(5);

                return Ok(new
                {
                    access_token = authHelper.CreateToken(claims, expiresAt),
                    expires_at = expiresAt,
                });
            }
            ModelState.AddModelError("Unauthorized", "You are not authorized to access this endpoint.");
            return BadRequest(ModelState);
        } 
    }
}
