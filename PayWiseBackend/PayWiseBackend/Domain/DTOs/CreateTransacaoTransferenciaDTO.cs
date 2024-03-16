namespace PayWiseBackend.Domain.DTOs;

public class CreateTransacaoTransferenciaDTO
{
    public double Valor { get; set; }
    public string? Descricao { get; set; }
    public int Pin { get; set; }
    public string ContaDestino { get; set; }
}
