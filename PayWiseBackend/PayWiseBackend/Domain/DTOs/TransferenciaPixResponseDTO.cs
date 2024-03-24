using System;

namespace PayWiseBackend.Domain.DTOs
{
    public class TransferenciaPixResponseDTO
    {
        public int Id { get; set; } // Identificador único da transferência PIX
        public decimal Valor { get; set; } // Valor da transferência PIX
        public string Descricao { get; set; } // Descrição da transferência PIX
        public string ChavePix { get; set; } // Chave PIX associada à transferência PIX
        public DateTime Horario { get; set; } // Horário da transferência PIX
        public int RemetenteId { get; set; } // Identificador do remetente da transferência PIX
        public int DestinatarioId { get; set; } // Identificador do destinatário da transferência PIX
        public bool Concluida { get; set; } // Indica se a transferência PIX foi concluída
        public DateTime? HorarioConclusao { get; set; } // Horário de conclusão da transferência PIX
    }
}
