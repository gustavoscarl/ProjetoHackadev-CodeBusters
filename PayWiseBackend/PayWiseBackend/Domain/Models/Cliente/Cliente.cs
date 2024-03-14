namespace PayWiseBackend.Domain.Models.Cliente;

public class Cliente : Entity
{
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Senha { get; set; } = null!;
    public string Cpf { get; set; } = null!;
    public string Rg { get; set; } = null!;
    public int EnderecoId { get; set; }
    public virtual Endereco Endereco { get; set; }

    public int TentativaLoginId { get; set; }
    public virtual TentativaLogin TentativaLogin { get; set; }

    public int DocumentoId { get; set; }
    public virtual Documento Documento { get; set; }
}
