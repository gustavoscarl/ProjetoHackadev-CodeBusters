using PayWiseBackend.Domain.Models;

namespace PayWiseBackend.Domain.DTOs;

public class RetrieveClienteDTO
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Sobrenome { get; set; }
    public string? Email { get; set; }
    public bool TemConta { get; set; }
    public bool TemCartao { get; set; }
    public int? ContaId { get; set; }
    public string? ContaUrl { get; set; }
}
