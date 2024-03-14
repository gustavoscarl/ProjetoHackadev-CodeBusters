using PayWiseBackend.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace PayWiseBackend.Domain.DTOs
{
    public class CreateContaDTO
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int Numero {  get; set; }

        public double Saldo { get; set; } = 0;

        public DateTime DataAbertura { get; set; } = DateTime.Now;

        public string Agencia { get; set; } = "1";

        public double LimitePixGeral { get; set; } = 0;

        public double LimitePixNoturno { get; set; } = 1000;


    }
}
