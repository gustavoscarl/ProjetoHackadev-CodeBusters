namespace PayWiseBackend.Domain.Models.Cliente;

public class TentativaLogin : Entity
{
    public int NumeroTentativas { get; set; }
    public DateTime? TempoBloqueio { get; set; } = null;
    public bool EstaBloqueado { get; set; } = false;
}
