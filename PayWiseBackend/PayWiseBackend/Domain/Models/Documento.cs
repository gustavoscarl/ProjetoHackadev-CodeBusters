namespace PayWiseBackend.Domain.Models;

public class Documento : Entity
{
    public string CpfImagem { get; set; } = null!;
    public string RgImagem { get; set; } = null!;
}
