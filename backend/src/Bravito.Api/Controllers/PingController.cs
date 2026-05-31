using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bravito.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PingController : ControllerBase
    {
        [HttpGet("/api/public/ping")]
        [AllowAnonymous]
        public IActionResult GetPublicPing()
        {
            return Ok(new { message = "Bravito API online" });
        }

        [HttpGet("/api/private/ping")]
        [Authorize]
        public IActionResult GetPrivatePing()
        {
            return Ok(new { message = "Acesso autenticado com sucesso" });
        }
    }
}
