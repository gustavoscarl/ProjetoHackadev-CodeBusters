namespace PayWiseBackend.Domain.DTOs;

public class RetrieveClienteDTO
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public bool temConta { get; set; }
    public bool temCartao { get; set; }
    public int? ContaId { get; set; }
}
