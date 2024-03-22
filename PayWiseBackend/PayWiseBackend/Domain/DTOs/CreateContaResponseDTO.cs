namespace PayWiseBackend.Domain.DTOs;

public class CreateContaResponseDTO
{
    public string Message { get; set; } = "Conta criada com sucesso!";
    public RetrieveContaDTO Conta { get; set; } = null!;
    public string AccessToken { get; set; } = null!;
}
