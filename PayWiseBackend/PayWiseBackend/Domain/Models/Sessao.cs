namespace PayWiseBackend.Domain.Models;

public class Sessao : Entity
{
    public string RefreshToken { get; set; } = null!;
    public int ClienteId { get; set; }
    public virtual Cliente? Cliente { get; set; }
}
