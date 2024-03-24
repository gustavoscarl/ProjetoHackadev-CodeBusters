using System;

namespace PayWiseBackend.Domain.DTOs
{
    public class TransferenciaPixDTO
    {
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public string ChavePix { get; set; }
    }
}
