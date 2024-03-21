using PayWiseBackend.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace PayWiseBackend.Domain.Models;

public class Transacao : Entity
{
    public TransacaoTipo Tipo { get; set; }
    public DateTime Horario { get; set; }
    public decimal Valor { get; set; }
    [MaxLength(200)]
    public string? Descricao { get; set; }
    public int HistoricoId { get; set; }
    public virtual Historico Historico { get; set; } = new();
}
