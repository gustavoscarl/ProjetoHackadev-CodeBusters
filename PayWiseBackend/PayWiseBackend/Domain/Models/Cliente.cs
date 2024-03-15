namespace PayWiseBackend.Domain.Models;

public class Cliente : Entity
{
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Senha { get; set; } = null!;
    public string Cpf { get; set; } = null!;
    public string Rg { get; set; } = null!;
    public bool temConta { get; set; } = false;
    public bool temCartao { get; set; } = false;
    public int EnderecoId { get; set; }
    public virtual Endereco Endereco { get; set; }

    public int TentativaLoginId { get; set; }
    public virtual TentativaLogin TentativaLogin { get; set; }

    public int DocumentoId { get; set; }
    public virtual Documento Documento { get; set; }

    public int? SessaoId { get; set; }
    public virtual Sessao Sessao { get; set; }

    public int? ContaId { get; set; }
    public virtual Conta Conta { get; set; }

}
