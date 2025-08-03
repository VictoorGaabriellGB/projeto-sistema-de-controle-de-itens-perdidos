using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Inova.Enums;

namespace Inova.Models
{
    public class UsuarioSemSenhaModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [Required(ErrorMessage = "O Login é obrigatório.")]
        public string Login { get; set; }
        [Required(ErrorMessage = "O Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O Email informado é inválido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O Celular é obrigatório.")]
        [Phone(ErrorMessage = "O número de celular informado é inválido.")]
        public string Celular { get; set; }
        [Required(ErrorMessage = "O Perfil é obrigatório.")]
        public PerfilEnum? Perfil { get; set; }
    }
}