using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Bravito.Application.Acesso.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Bravito.Infrastructure.Acesso.Services
{
    public class UsuarioAtualService : IUsuarioAtualService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioAtualService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? ObterKeycloakId()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public string? ObterEmail()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
        }

        public string? ObterNome()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst("name")?.Value ??
                   _httpContextAccessor.HttpContext?.User?.FindFirst("preferred_username")?.Value;
        }

        public IReadOnlyCollection<string> ObterRoles()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null) return new List<string>();

            return user.Claims
                .Where(c => c.Type == ClaimTypes.Role || c.Type == "realm_access")
                .Select(c => c.Value)
                .ToList();
        }
    }
}
