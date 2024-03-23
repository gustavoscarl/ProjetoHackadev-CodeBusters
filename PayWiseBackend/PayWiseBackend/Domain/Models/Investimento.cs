namespace PayWiseBackend.Domain.Models;

public class Investimento : Entity
{
    public decimal Valor { get; set; }
    public decimal Taxa { get; set; } = 2.00m;
    public DateTime Tempo { get; set; }
    public int ContaId { get; set; }
    public virtual Conta? Conta { get; set; }
}
