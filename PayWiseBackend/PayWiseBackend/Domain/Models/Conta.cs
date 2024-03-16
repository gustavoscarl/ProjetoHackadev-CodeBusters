using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayWiseBackend.Domain.Models;

public class Conta : Entity
{
    [MinLength(6)]
    [MaxLength(6)]
    public int Numero { get; set; }
    public double Saldo { get; set; } = 0.00;
    public DateTime DataAbertura { get; set; } = DateTime.Now;
    public DateTime DataModificacao { get; set; } = DateTime.Now;
    public string Agencia { get; set; } = "0056";
    public int Pin { get; set; }
    public double LimitePixGeral { get; set; } = 1000.00;
    public double LimitePixNoturno { get; set; } = 1000.00;

    public int HistoricoId { get; set; }
    public virtual Historico Historico { get; set; }
}
