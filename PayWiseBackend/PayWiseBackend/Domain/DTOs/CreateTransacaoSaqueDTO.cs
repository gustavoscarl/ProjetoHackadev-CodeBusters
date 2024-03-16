namespace PayWiseBackend.Domain.DTOs;

public class CreateTransacaoSaqueDTO
{
    public double Valor { get; set; }
    public string? Descricao { get; set; }
    public int Pin { get; set; }
}
