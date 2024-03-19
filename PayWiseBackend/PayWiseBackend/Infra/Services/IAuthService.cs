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
    string GenerateToken(int clienteId, TokenType type);
    string GenerateAccessToken(int clienteId);
    string GenerateRefreshToken(int clienteId);
    int? GetClienteIdFromAccessToken(string accessToken);
    string HashPassword(string senha);
    Task SalvarSessao(int ClienteId, string refreshToken);
}
