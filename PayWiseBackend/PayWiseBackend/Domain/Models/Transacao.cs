namespace PayWiseBackend.Domain.Models;

public class Transacao : Entity
{
    public string Tipo { get; set; }
    public DateTime Horario { get; set; }
    public double Valor { get; set; }
    public string Descricao { get; set; }
    public int HistoricoId { get; set; }
    public virtual Historico Historico { get; set; }
}
