using Bunzl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bunzl.Infra.CrossCutting.Resources;

namespace Bunzl.Infra.Data.Configurations;

public class NegociacaoComercialProdutoConfiguration : IEntityTypeConfiguration<NegociacaoComercialProduto>
{
    public void Configure(EntityTypeBuilder<NegociacaoComercialProduto> builder)
    {
        builder.HasKey(ncp => ncp.Id);

        builder.Property(ncp => ncp.ProdutoId)
            .IsRequired();

        builder.Property(ncp => ncp.CodigoSku)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(ncp => ncp.Descricao)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(ncp => ncp.Quantidade)
            .HasColumnType("decimal(18,5)")
            .IsRequired();

        builder.Property(ncp => ncp.ValorUnitarioOriginal)
            .HasColumnType("decimal(18,6)")
            .IsRequired();

        builder.Property(ncp => ncp.ValorUnitarioNegociado)
            .HasColumnType("decimal(18,6)")
            .IsRequired();

        builder.Property(ncp => ncp.ValorUnitarioAlvo)
           .HasColumnType("decimal(18,6)")
           .IsRequired();

        builder.Property(ncp => ncp.ValorUnitarioFinal)
           .HasColumnType("decimal(18,6)")
           .IsRequired();

        builder.Property(ncp => ncp.ValorTotal)
            .HasColumnType("decimal(18,6)")
            .IsRequired();

        builder.Property(ncp => ncp.Observacao)
            .HasMaxLength(150);

        builder.Property(ncp => ncp.NegociacaoComercialId)
            .IsRequired();

        builder.HasOne(ncp => ncp.NegociacaoComercial)
            .WithMany(nc => nc.Produtos)
            .HasForeignKey(ncp => ncp.NegociacaoComercialId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(TabelasResources.NegociacaoComercialProduto);
    }
}

