using PayWiseBackend.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace PayWiseBackend.Domain.DTOs;

public class CreateEnderecoDTO
{
    [MaxLength(50)]
    public string Rua { get; set; } = string.Empty;
    public int? Numero { get; set; }
    [MaxLength(50)]
    public string Bairro { get; set; } = string.Empty;
    [MaxLength(200)]
    public string Complemento { get; set; } = string.Empty;
    [MaxLength(8)]
    public string Cep { get; set; } = string.Empty;
    [MaxLength(50)]
    public string Cidade { get; set; } = string.Empty;
    public Estado Estado { get; set; }
}
