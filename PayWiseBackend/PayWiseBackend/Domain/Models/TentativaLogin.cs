namespace PayWiseBackend.Domain.Models;

public class TentativaLogin : Entity
{
    public int NumeroTentativas { get; set; }
    public DateTime? TempoBloqueio { get; set; } = null;
    public bool EstaBloqueado { get; set; } = false;
    public virtual Cliente? Cliente { get; set; }
}
