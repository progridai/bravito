using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Bravito.Infrastructure.Data;
using Bravito.Api.Filters;

namespace Bravito.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AcessoController : ControllerBase
    {
        private readonly BravitoDbContext _context;

        public AcessoController(BravitoDbContext context)
        {
            _context = context;
        }

        [HttpGet("recursos")]
        [RequerRecurso("usuarios.visualizar")]
        public async Task<IActionResult> GetRecursos()
        {
            var recursos = await _context.Recursos
                .OrderBy(r => r.Codigo)
                .Select(r => new
                {
                    r.Id,
                    r.Codigo,
                    r.Nome,
                    r.Descricao,
                    r.Ativo
                })
                .ToListAsync();

            return Ok(recursos);
        }

        [HttpGet("perfis")]
        [RequerRecurso("usuarios.visualizar")]
        public async Task<IActionResult> GetPerfis()
        {
            var perfis = await _context.PerfisAcesso
                .Include(p => p.Recursos)
                .ThenInclude(pr => pr.Recurso)
                .OrderBy(p => p.Nome)
                .Select(p => new
                {
                    p.Id,
                    p.Nome,
                    p.Descricao,
                    p.Ativo,
                    Recursos = p.Recursos.Select(pr => pr.Recurso.Codigo).ToList()
                })
                .ToListAsync();

            return Ok(perfis);
        }
    }
}
