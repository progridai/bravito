using System.ComponentModel.DataAnnotations;

namespace Bravito.Application.Acesso.Models
{
    public class AlterarSenhaRequest
    {
        [Required(ErrorMessage = "A nova senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
        public string NovaSenha { get; set; } = string.Empty;
    }
}
