using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bravito.Application.Acesso.Models
{
    public class EditarUsuarioRequest
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(255)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        public List<Guid> PerfilIds { get; set; } = new List<Guid>();

        public bool Ativo { get; set; } = true;
    }
}
