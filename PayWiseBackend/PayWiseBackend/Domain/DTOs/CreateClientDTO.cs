using PayWiseBackend.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace PayWiseBackend.Domain.DTOs
{
    public class CreateClientDTO
    {
        [Required]
        [MaxLength(200)]
        public string Nome { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(8)]
        [StringLength(35)]
        public string Senha { get; set; } = null!;

        [Required]
        [MinLength(11)]
        [StringLength(11)]
        public string Cpf { get; set; } = null!;

        [Required]
        [MinLength(5)]
        [StringLength(11)]
        public string Rg { get; set; } = null!;

        [Required]
        public Endereco Endereco { get; set; } = null!;
    }
}
