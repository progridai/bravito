using System;
using System.Threading;
using System.Threading.Tasks;
using Bravito.Api.Filters;
using Bravito.Application.Acesso.Interfaces;
using Bravito.Application.Acesso.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bravito.Api.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    [Authorize]
    public class UsuariosAdminController : ControllerBase
    {
        private readonly IUsuariosAdminService _usuariosAdminService;

        public UsuariosAdminController(IUsuariosAdminService usuariosAdminService)
        {
            _usuariosAdminService = usuariosAdminService;
        }

        [HttpGet]
        [RequerRecurso("usuarios.visualizar")]
        public async Task<IActionResult> ListarUsuarios(CancellationToken cancellationToken)
        {
            var usuarios = await _usuariosAdminService.ListarUsuariosAsync(cancellationToken);
            return Ok(usuarios);
        }

        [HttpGet("{id:guid}")]
        [RequerRecurso("usuarios.visualizar")]
        public async Task<IActionResult> ObterUsuario(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var usuario = await _usuariosAdminService.ObterPorIdAsync(id, cancellationToken);
                return Ok(usuario);
            }
            catch (System.Collections.Generic.KeyNotFoundException ex)
            {
                return NotFound(new { erro = ex.Message });
            }
        }

        [HttpPost]
        [RequerRecurso("usuarios.cadastrar")]
        public async Task<IActionResult> CriarUsuario([FromBody] CriarUsuarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var usuario = await _usuariosAdminService.CriarUsuarioAsync(request, cancellationToken);
                return CreatedAtAction(nameof(ObterUsuario), new { id = usuario.Id }, usuario);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro interno ao criar usuário. " + ex.Message });
            }
        }

        [HttpPut("{id:guid}")]
        [RequerRecurso("usuarios.editar")]
        public async Task<IActionResult> EditarUsuario(Guid id, [FromBody] EditarUsuarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var usuario = await _usuariosAdminService.EditarUsuarioAsync(id, request, cancellationToken);
                return Ok(usuario);
            }
            catch (System.Collections.Generic.KeyNotFoundException ex)
            {
                return NotFound(new { erro = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro interno ao atualizar usuário. " + ex.Message });
            }
        }

        [HttpPatch("{id:guid}/ativar")]
        [RequerRecurso("usuarios.editar")] // Ou criar uma policia q aceita usuarios.editar OR usuarios.desativar. Mantendo conforme a documentacao, vamos usar usuarios.desativar q eh a especifica para isso. (mas a doc sugeriu um ou outro, o filtro de RequerRecurso n suporta multiplos com OR de forma facil, entao vou exigir usuarios.desativar).
        [RequerRecurso("usuarios.desativar")]
        public async Task<IActionResult> AtivarUsuario(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var usuario = await _usuariosAdminService.AlterarStatusUsuarioAsync(id, true, cancellationToken);
                return Ok(usuario);
            }
            catch (System.Collections.Generic.KeyNotFoundException ex)
            {
                return NotFound(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro interno ao ativar usuário." });
            }
        }

        [HttpPatch("{id:guid}/desativar")]
        [RequerRecurso("usuarios.desativar")]
        public async Task<IActionResult> DesativarUsuario(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var usuario = await _usuariosAdminService.AlterarStatusUsuarioAsync(id, false, cancellationToken);
                return Ok(usuario);
            }
            catch (System.Collections.Generic.KeyNotFoundException ex)
            {
                return NotFound(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro interno ao desativar usuário." });
            }
        }
    }
}
