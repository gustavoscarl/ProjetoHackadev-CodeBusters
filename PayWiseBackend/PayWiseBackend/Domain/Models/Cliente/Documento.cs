namespace PayWiseBackend.Domain.Models.Cliente;

public class Documento : Entity
{
    public string CpfImagem { get; set; } = null!;
    public string RgImagem { get; set; } = null!;
}
