namespace PayWiseBackend.Domain.DTOs;

public class RetrieveContaDTO
{
    public int Id { get; set; }
    public string? Numero { get; set; }
    public double Saldo { get; set; }
    public string? Agencia { get; set; }
    public string? HistoricoUrl { get; set; }
}
