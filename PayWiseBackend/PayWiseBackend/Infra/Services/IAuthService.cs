using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Models;

namespace PayWiseBackend.Infra.Services;

public enum TokenType
{
    Access,
    Refresh
}

public interface IAuthService
{
    Task<Cliente?> ValidateCredentials(CreateLoginDTO loginCredentials);
    string GenerateToken(int clienteId, TokenType type, int? contaId);
    string GenerateAccessToken(int clienteId, int? contaId);
    string GenerateRefreshToken(int clienteId, int? contaId);
    int? GetClienteIdFromToken(string token);
    int? GetContaIdFromAccessToken(string accessToken);
    string HashPassword(string senha);
    Task SalvarSessao(int ClienteId, string refreshToken);
}
