namespace PayWiseBackend.Domain.DTOs;

public class RetrieveHistoricoDTO
{
    public List<RetrieveTransacaoDTO> Transacoes { get; set; } = new();
}
