using Bravito.Infrastructure.Integrations.N8n.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Bravito.Api.Controllers
{
    [ApiController]
    [Route("health")]
    public class HealthController : ControllerBase
    {
        private readonly N8nOptions _n8nOptions;

        public HealthController(IOptions<N8nOptions> n8nOptions)
        {
            _n8nOptions = n8nOptions.Value;
        }

        [HttpGet("n8n")]
        public IActionResult CheckN8nHealth()
        {
            if (string.IsNullOrWhiteSpace(_n8nOptions.WebhookUrl))
            {
                return StatusCode(500, new { status = "Unhealthy", details = "Webhook URL do n8n não está configurada." });
            }

            return Ok(new { status = "Healthy", details = "Configuração do n8n presente." });
        }
    }
}
