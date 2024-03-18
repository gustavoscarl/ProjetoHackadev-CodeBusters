using System.ComponentModel.DataAnnotations;

namespace PayWiseBackend.Domain.DTOs;

public class CreateContaDTO
{
    [Required]
    public int Pin { get; set; }
}
