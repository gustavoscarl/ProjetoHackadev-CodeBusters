namespace PayWiseBackend.Domain.Models;

public class Documento : Entity
{
    public string CpfImagem { get; set; } = null!;
    public string RgImagem { get; set; } = null!;
    public DateTime CriadoEm { get; set; } = DateTime.Now;
    public DateTime AtualizadoEm { get; set; } = DateTime.Now;
    public int ClienteId { get; set; }
    public virtual Cliente? Cliente { get; set; }
}
