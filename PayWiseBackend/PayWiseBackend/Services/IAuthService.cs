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
    Task<Cliente?> ValidateCredentials(CreateLoginDTO loginCredentials);
    string GenerateToken(int clienteId, TokenType type, int? contaId);
    string GenerateAccessToken(int clienteId, int? contaId);
    string GenerateRefreshToken(int clienteId, int? contaId);
    int? GetClienteIdFromAccessToken(string accessToken);
    int? GetContaIdFromAccessToken(string accessToken);
}
