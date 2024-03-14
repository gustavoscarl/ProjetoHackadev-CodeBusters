using Microsoft.EntityFrameworkCore;
using PayWiseBackend.Domain.Models.Cliente;

namespace PayWiseBackend.Domain.Context;

public class PaywiseDbContext : DbContext
{
    public PaywiseDbContext(DbContextOptions<PaywiseDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<TentativaLogin> TentativasLogin { get; set; }
    public DbSet<Documento> Documentos { get; set; }
}
