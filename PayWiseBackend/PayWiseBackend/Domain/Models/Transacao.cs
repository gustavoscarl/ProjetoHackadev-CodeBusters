using PayWiseBackend.Domain.Enum;

namespace PayWiseBackend.Domain.Models;

public class Transacao : Entity
{
    public TransacaoTipo Tipo { get; set; }
    public DateTime Horario { get; set; }
    public double Valor { get; set; }
    public string Descricao { get; set; }
    public int HistoricoId { get; set; }
    public virtual Historico Historico { get; set; }
}
