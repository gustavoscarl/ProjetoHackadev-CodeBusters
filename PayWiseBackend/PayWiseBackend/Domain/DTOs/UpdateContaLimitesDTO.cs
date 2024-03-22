namespace PayWiseBackend.Domain.DTOs;

public class UpdateContaLimitesDTO
{
    public double LimitePixGeral { get; set; } = 1000.00;
    public double LimitePixNoturno { get; set; } = 1000.00;
    public int Pin { get; set; }
}
