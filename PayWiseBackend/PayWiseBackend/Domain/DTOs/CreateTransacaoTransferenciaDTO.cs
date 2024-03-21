using System.ComponentModel.DataAnnotations;

namespace PayWiseBackend.Domain.DTOs;

public class CreateTransacaoTransferenciaDTO
{
    [Required]
    public decimal Valor { get; set; }
    [MaxLength(200)]
    public string? Descricao { get; set; }
    [Required]
    public int Pin { get; set; }
    [Required]
    public string ContaDestino { get; set; } = null!;
}
