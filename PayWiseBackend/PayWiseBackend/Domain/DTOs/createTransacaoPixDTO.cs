using System.ComponentModel.DataAnnotations;

namespace PayWiseBackend.Domain.DTOs
{
   public class CreateTransacaoPixDTO
{
    public CreateTransacaoPixDTO()
    {
        NumeroContaDestino = string.Empty;
        Descricao = string.Empty;
    }

    [Required(ErrorMessage = "O número da conta de destino é obrigatório.")]
    public string NumeroContaDestino { get; set; }

    [Required(ErrorMessage = "O valor da transação é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor da transação deve ser maior que zero.")]
    public decimal Valor { get; set; }

    [MaxLength(200, ErrorMessage = "A descrição da transação deve ter no máximo 200 caracteres.")]
    public string Descricao { get; set; }
}
}
