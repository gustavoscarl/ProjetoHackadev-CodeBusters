using System;
using System.ComponentModel.DataAnnotations;

namespace PayWiseBackend.Domain.Models
{
    public class TransferenciaPIX : Entity
    {
        public DateTime Horario { get; set; }
        public decimal Valor { get; set; }
        [MaxLength(200)]
        public string? Descricao { get; set; }

        // Informações do remetente
        public int RemetenteId { get; set; }
        public virtual Conta Remetente { get; set; }

        // Informações do destinatário
        public int DestinatarioId { get; set; }
        public virtual Conta Destinatario { get; set; }

        // Status da transferência PIX
        public bool Concluida { get; set; }
        public DateTime? HorarioConclusao { get; set; }
    }
}
