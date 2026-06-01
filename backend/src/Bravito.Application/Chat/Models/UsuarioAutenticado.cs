namespace Bravito.Application.Chat.Models
{
    public class UsuarioAutenticado
    {
        public string Id { get; set; } = string.Empty;
        public string NomeUsuario { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? EmpresaId { get; set; }
    }
}
