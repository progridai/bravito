using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bravito.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpGet("me")]
        [Authorize]
        public IActionResult GetMe()
        {
            var user = HttpContext.User;

            var roles = user.Claims
                .Where(c => c.Type == ClaimTypes.Role || c.Type == "realm_access")
                .Select(c => c.Value)
                .ToList();

            var userInfo = new
            {
                Id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                Username = user.FindFirst("preferred_username")?.Value,
                Name = user.FindFirst("name")?.Value,
                Email = user.FindFirst(ClaimTypes.Email)?.Value,
                Roles = roles
            };

            return Ok(userInfo);
        }
    }
}
