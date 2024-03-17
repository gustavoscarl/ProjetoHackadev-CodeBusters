using System.ComponentModel.DataAnnotations;

namespace PayWiseBackend.Domain.DTOs;

public class CreateTransacaoSaqueDTO
{
    [Required]
    public double Valor { get; set; }
    [MaxLength(200)]
    public string? Descricao { get; set; }
    [Required]
    public int Pin { get; set; }
}
