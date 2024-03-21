using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PayWiseBackend.Domain.Context;
using PayWiseBackend.Domain.DTOs;
using PayWiseBackend.Domain.Models;

namespace PayWiseBackend.Infra.Services;

public class ClienteService : IClienteService
{
    private readonly PaywiseDbContextSqlite _contextSqlite;
    private readonly IMapper _mapper;

    public ClienteService(
        PaywiseDbContextSqlite contextSqlite,
        IMapper mapper
        )
    {
        _contextSqlite = contextSqlite;
        _mapper = mapper;
    }

    public async Task<Cliente> BuscarClientePorId(int? clienteId)
    {
        var cliente = await _contextSqlite.Clientes.FindAsync(clienteId);
        return cliente;
    }

    public async Task<RetrieveClienteDTO> CadastrarCliente(CreateClientDTO novoCliente)
    {
        var clienteCadastrar = _mapper.Map<Cliente>(novoCliente);

        var result = _contextSqlite.Clientes.Add(clienteCadastrar);
        await _contextSqlite.SaveChangesAsync();

        var cliente = result.Entity;

        var clienteSalvo = _mapper.Map<RetrieveClienteDTO>(cliente);
        return clienteSalvo;
    }

    public async Task<bool> CheckClienteCredentials(string cpf, string rg)
    {
        
        bool doesClienteAlreadyExist = await _contextSqlite.Clientes.AnyAsync(cliente => cliente.Cpf == cpf || cliente.Rg == rg);
        if (doesClienteAlreadyExist)
            return true;

        return false;
    }
}
