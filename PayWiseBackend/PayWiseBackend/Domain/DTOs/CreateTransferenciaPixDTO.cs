using System.ComponentModel.DataAnnotations;

namespace PayWiseBackend.Domain.DTOs
{
    public class CreateTransferenciaPixDTO
    {
        [Required]
        public decimal Valor { get; set; }

        [MaxLength(200)]
        public string Descricao { get; set; }

        [Required]
        public int DestinatarioId { get; set; }
    }
}
