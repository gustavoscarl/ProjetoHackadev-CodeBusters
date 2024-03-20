﻿using Microsoft.EntityFrameworkCore;
using PayWiseBackend.Domain.Models;

namespace PayWiseBackend.Domain.Context;

public class PaywiseDbContext : DbContext
{
    public PaywiseDbContext(DbContextOptions<PaywiseDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cliente>()
            .HasOne(cliente => cliente.Conta)
            .WithOne(conta => conta.Cliente)
            .HasForeignKey<Conta>(conta => conta.ClienteId);

        modelBuilder.Entity<Cliente>()
            .HasOne(cliente => cliente.Endereco)
            .WithOne(endereco => endereco.Cliente)
            .HasForeignKey<Endereco>(endereco => endereco.ClienteId);

        modelBuilder.Entity<Cliente>()
            .HasOne(cliente => cliente.Documento)
            .WithOne(documento => documento.Cliente)
            .HasForeignKey<Documento>(documento => documento.ClienteId);

        modelBuilder.Entity<Cliente>()
            .HasOne(cliente => cliente.Sessao)
            .WithOne(sessao => sessao.Cliente)
            .HasForeignKey<Sessao>(sessao => sessao.ClienteId);

        modelBuilder.Entity<Conta>()
            .HasOne(conta => conta.Historico)
            .WithOne(historico => historico.Conta)
            .HasForeignKey<Historico>(historico => historico.ContaId);

        modelBuilder.Entity<Historico>()
            .HasMany(historico => historico.Transacoes)
            .WithOne(transacao => transacao.Historico)
            .HasForeignKey(transacao => transacao.HistoricoId);
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<TentativaLogin> TentativasLogin { get; set; }
    public DbSet<Documento> Documentos { get; set; }
    public DbSet<Sessao> Sessoes { get; set; }
    public DbSet<Conta> Contas { get; set; }
    public DbSet<Transacao> Transacoes { get; set; }
    public DbSet<Historico> Historicos { get; set; }
}
