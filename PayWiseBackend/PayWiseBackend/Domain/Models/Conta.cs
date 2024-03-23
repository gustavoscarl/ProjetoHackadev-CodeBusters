using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayWiseBackend.Domain.Models;

public class Conta : Entity
{
    [MinLength(6)]
    [MaxLength(6)]
    public string Numero { get; set; } = null!;
    public decimal Saldo { get; set; } = 0.00m;
    public DateTime DataAbertura { get; set; } = DateTime.Now;
    public DateTime DataModificacao { get; set; } = DateTime.Now;
    public bool EstaAtiva { get; set; } = true;
    [MinLength(4)]
    [MaxLength(4)]
    public string Agencia { get; set; } = "0056";
    public int Pin { get; set; }
    public double LimitePixGeral { get; set; } = 1000.00;
    public double LimitePixNoturno { get; set; } = 1000.00;
    public virtual Historico Historico { get; set; } = new();
    public int ClienteId { get; set; }
    public virtual Cliente? Cliente { get; set; }
    public virtual Investimento? Investimento { get; set; }
}
