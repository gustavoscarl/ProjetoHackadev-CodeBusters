using PayWiseBackend.Domain.DTOs;
using System.ComponentModel.DataAnnotations;

namespace PayWiseBackend.Domain.Models;

public class Cliente : Entity
{
    [Required]
    [MaxLength(100)]
    public string Nome { get; set; } = null!;
    [Required]
    [MaxLength(150)]
    public string Sobrenome { get; set; } = null!;
    [Required]
    [EmailAddress]
    [MaxLength(50)]
    public string Email { get; set; } = null!;
    [Required]
    [StringLength(150)]
    public string Senha { get; set; } = null!;
    [Required]
    [StringLength(11)]
    public string Cpf { get; set; } = null!;
    [Required]
    [MinLength(5)]
    [StringLength(11)]
    public string Rg { get; set; } = null!;
    public bool TemConta { get; set; } = false;
    public bool TemCartao { get; set; } = false;
    public DateTime CriadoEm { get; set; } = DateTime.Now;
    public DateTime AtualizadoEm { get; set; } = DateTime.Now;
    public bool EstaAtivo { get; set; } = true;
    public virtual Endereco Endereco { get; set; } = null!;

    public virtual TentativaLogin? TentativaLogin { get; set; }

    public virtual Documento? Documento { get; set; }

    public virtual Sessao? Sessao { get; set; }
    public virtual Conta? Conta { get; set; }

}
