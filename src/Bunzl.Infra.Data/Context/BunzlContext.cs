using Bunzl.Core.Domain.Interfaces.Base;
using Bunzl.Domain.Entities;
using Bunzl.Infra.CrossCutting.Extensions;
using Bunzl.Infra.CrossCutting.NotificationPattern.DTOs;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Bunzl.Infra.Data.Context;

public class BunzlContext(DbContextOptions<BunzlContext> options, IUsuarioAutenticado usuarioAutenticado) : DbContext(options)
{
    private Guid EmpresaIdConectada { get; } = usuarioAutenticado?.UsuarioEmpresa ?? Guid.Empty;
    public DbSet<Usuario> UsuarioSet { get; set; }
    public DbSet<Empresa> EmpresaSet { get; set; }
    public DbSet<Fornecedor> FornecedorSet { get; set; }
    public DbSet<FornecedorDadoBancario> FornecedorDadoBancarioSet { get; set; }
    public DbSet<FornecedorDocumento> FornecedorDocumentoSet { get; set; }
    public DbSet<FornecedorObservacao> FornecedorObservacaoSet { get; set; }
    public DbSet<FornecedorProduto> FornecedorProdutoSet { get; set; }
    public DbSet<FornecedorProdutoAnexo> FornecedorProdutoAnexoSet { get; set; }
    public DbSet<TabelaPreco> TabelaPrecoSet { get; set; }
    public DbSet<TabelaPrecoProduto> TabelaPrecoProdutoSet { get; set; }
    public DbSet<NegociacaoComercial> NegociacaoComercialSet { get; set; }
    public DbSet<NegociacaoComercialProduto> NegociacaoComercialProdutoSet { get; set; }
    public DbSet<NegociacaoComercialAnexo> NegociacaoComercialAnexoSet { get; set; }
    public DbSet<OrdemDeCompra> OrdemDeCompraSet { get; set; }
    public DbSet<OrdemDeCompraProduto> OrdemDeCompraProdutoSet { get; set; }
    public DbSet<OrdemDeCompraAnexo> OrdemDeCompraAnexoSet { get; set; }
    public DbSet<OrdemDeCompraObservacao> OrdemDeCompraObservacaoSet { get; set; }
    public DbSet<OrdemDeCompraUnidadeMedida> OrdemDeCompraUnidadeMedidaSet { get; set; }
    public DbSet<Moeda> MoedaSet { get; set; }
    public DbSet<Incoterm> IncotermSet { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Notification>();
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Usuario>().HasQueryFilter(u => EmpresaIdConectada == Guid.Empty || u.Empresas.Any(e => e.Id == EmpresaIdConectada));

        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        SetInfoAdded();
        SetInfoUpdated();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetInfoAdded();
        SetInfoUpdated();
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<Enum>()
            .HaveConversion<string>();
    }

    protected virtual void SetInfoAdded()
    {
        GetAddedEntities()
            .ForEach(entity =>
            {
                entity.DataCriacao = DateTime.UtcNow;
                entity.UsuarioCriacao = usuarioAutenticado?.UsuarioId ?? Guid.Empty;
            });
    }

    protected virtual void SetInfoUpdated()
    {
        GetUpdatedEntities()
            .ForEach(entity =>
            {
                entity.DataAlteracao = DateTime.UtcNow;
                entity.UsuarioAlteracao = usuarioAutenticado?.UsuarioId ?? Guid.Empty;
            });
    }

    private IEnumerable<IEntityBase> GetAddedEntities()
    {
        return from e in ChangeTracker.Entries()
               where e.Entity is IEntityBase && e.State == EntityState.Added
               select (IEntityBase)e.Entity;
    }

    private IEnumerable<IEntityBase> GetUpdatedEntities()
    {
        return from e in ChangeTracker.Entries()
               where e.Entity is IEntityBase && e.State == EntityState.Modified
               select (IEntityBase)e.Entity;
    }
}