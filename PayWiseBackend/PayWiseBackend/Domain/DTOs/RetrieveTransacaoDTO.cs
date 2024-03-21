using PayWiseBackend.Domain.Enum;

namespace PayWiseBackend.Domain.DTOs;

public class RetrieveTransacaoDTO
{
    public TransacaoTipo Tipo { get; set; }
    public DateTime Horario { get; set; }
    public decimal Valor { get; set; }
    public string? Descricao { get; set; }
    public string? TransacaoUrl { get; set; }
}
