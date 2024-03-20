using System.ComponentModel.DataAnnotations;

namespace PayWiseBackend.Domain.DTOs;

public class CreateTransacaoDepositoDTO
{
    [Required]
    public decimal Valor { get; set; }
    [MaxLength(200)]
    public string? Descricao { get; set; }
}
