namespace PayWiseBackend.Domain.DTOs;

public class CreateInvestimentoDTO
{
    public decimal Valor { get; set; }
    public DateTime Tempo { get; set; } = DateTime.Now.AddMonths(1).Date;
}
