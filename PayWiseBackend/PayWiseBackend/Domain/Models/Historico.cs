namespace PayWiseBackend.Domain.Models;

public class Historico : Entity
{
    public List<Transacao> Transacoes { get; set; }
}
