using System.ComponentModel.DataAnnotations;

namespace PayWiseBackend.Domain.DTOs;

public class LoginRequestDTO
{
    [Required]
    [MinLength(11)]
    [MaxLength(11)]
    public string Cpf { get; set; } = null!;
    [Required]
    [MinLength(8)]
    [MaxLength(12)]
    public string Senha { get; set; } = null!;

}
