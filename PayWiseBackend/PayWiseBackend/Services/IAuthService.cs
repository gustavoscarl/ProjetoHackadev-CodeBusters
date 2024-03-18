using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Models;

namespace PayWiseBackend.Services;

public enum TokenType
{
    Access,
    Refresh
}

public interface IAuthService
{
    Task<Cliente?> ValidateCredentials(LoginRequestDTO loginCredentials);
    string GenerateToken(int clienteId, TokenType type);
    string GenerateAccessToken(int clienteId);
    string GenerateRefreshToken(int clienteId);
    int? GetClienteIdFromAccessToken(string accessToken);
}
