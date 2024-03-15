using PayWiseBackend.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace PayWiseBackend.Domain.DTOs
{
    public class CreateClientDTO
    {
        [Required]
        [MaxLength(200)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Senha { get; set; }

        [Required]
        [MinLength(11)]
        [MaxLength(11)]
        public string Cpf { get; set; }

        [Required]
        [MaxLength(20)]
        public string Rg { get; set;}

        [Required]
        public Endereco Endereco { get; set; }
    }
}
