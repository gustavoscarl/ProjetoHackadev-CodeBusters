namespace PayWiseBackend.Domain.Models;

public class Sessao : Entity
{
    public string RefreshToken { get; set; } = null!;
}
