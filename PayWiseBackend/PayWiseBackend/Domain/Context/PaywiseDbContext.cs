using Microsoft.EntityFrameworkCore;
using PayWiseBackend.Domain.Models;

namespace PayWiseBackend.Domain.Context;

public class PaywiseDbContext : DbContext
{
    public PaywiseDbContext(DbContextOptions<PaywiseDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<TentativaLogin> TentativasLogin { get; set; }
    public DbSet<Documento> Documentos { get; set; }
    public DbSet<Sessao> Sessoes { get; set; }
    public DbSet<Conta> Contas { get; set; }
    public DbSet<Transacao> Transacoes { get; set; }
    public DbSet<Historico> Historicos { get; set; }
}
