using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Inova.Models
{
    public class TabelaModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O Nome do item é obrigatório.")]
        public string NomeItem { get; set; }
        [Required(ErrorMessage = "O Tipo do item é obrigatório.")]
        public string Tipo { get; set; }
        [Required(ErrorMessage = "O Nome da Pessoa é obrigatório.")]
        public string NomePessoa { get; set; }
        [Required(ErrorMessage = "O Celular é obrigatório.")]
        [Phone(ErrorMessage = "O número de celular informado é inválido.")]
        public string Celular { get; set; }
        [Required(ErrorMessage = "O Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O Email informado é inválido.")]
        public string Email { get; set; }
        public DateTime DataAchado { get; set; }
        public DateTime DataCadastro { get; set; }
        
    }
}