using System.ComponentModel.DataAnnotations;

namespace PayWiseBackend.Domain.Models;

public class Cliente : Entity
{
    [Required]
    [MaxLength(200)]
    public string Nome { get; set; } = null!;
    [Required]
    [EmailAddress]
    [MaxLength(50)]
    public string Email { get; set; } = null!;
    [Required]
    [MinLength(8)]
    [StringLength(12)]
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
    public int EnderecoId { get; set; }
    public virtual Endereco Endereco { get; set; }

    public int? TentativaLoginId { get; set; }
    public virtual TentativaLogin TentativaLogin { get; set; }

    public int? DocumentoId { get; set; }
    public virtual Documento Documento { get; set; }

    public int? SessaoId { get; set; }
    public virtual Sessao Sessao { get; set; }

    public int? ContaId { get; set; }
    public virtual Conta Conta { get; set; }

}
