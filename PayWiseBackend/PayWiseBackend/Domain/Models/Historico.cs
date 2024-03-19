namespace PayWiseBackend.Domain.Models;

public class Historico : Entity
{
    public int ContaId { get; set; }
    public virtual Conta? Conta { get; set; }
    public virtual List<Transacao> Transacoes { get; set; } = new();
}
