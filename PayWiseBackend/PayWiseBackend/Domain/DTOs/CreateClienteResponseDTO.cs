namespace PayWiseBackend.Domain.DTOs;

public class CreateClienteResponseDTO
{
    public string Message { get; set; } = "Cliente cadastrada(o) com sucesso.";
    public RetrieveClienteDTO Cliente { get; set; } = null!;
}
