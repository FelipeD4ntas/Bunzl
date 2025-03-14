using Bunzl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bunzl.Infra.CrossCutting.Resources;

namespace Bunzl.Infra.Data.Configurations;

public class OrdemDeCompraProdutoConfiguration : IEntityTypeConfiguration<OrdemDeCompraProduto>
{
    public void Configure(EntityTypeBuilder<OrdemDeCompraProduto> builder)
    {
        builder.HasKey(ocp => ocp.Id);

        builder.Property(ocp => ocp.OrdemDeCompraId);

        builder.Property(p => p.OrdemItem)
            .IsRequired(false)
            .HasMaxLength(50);

        builder.Property(p => p.CodigoItem)
            .IsRequired(false)
            .HasMaxLength(50);

        builder.Property(ocp => ocp.CodigoSKU)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(ocp => ocp.Descricao)
            .IsRequired(false);

        builder.Property(ocp => ocp.UnidadeMedida)
            .IsRequired(false)
            .HasMaxLength(50);

        builder.Property(ocp => ocp.CodigoNCM)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(ocp => ocp.Quantidade)
            .IsRequired(false)
            .HasColumnType("decimal(18,2)");

        builder.Property(ocp => ocp.MoedaSigla)
            .IsRequired(false)
            .HasMaxLength(10);

        builder.Property(ocp => ocp.ValorUnitario)
            .IsRequired(false)
            .HasColumnType("decimal(18,6)");

        builder.Property(ocp => ocp.ValorTotal)
            .IsRequired(false)
            .HasColumnType("decimal(18,6)");

        builder.Property(ocp => ocp.DataEstimadaPartida)
            .IsRequired(false)
            .HasColumnType("date");

        builder.Property(ocp => ocp.Etd)
            .IsRequired(false);

        builder.Property(ocp => ocp.NumeroLote)
            .IsRequired(false)
            .HasMaxLength(50);

        builder.Property(ocp => ocp.PosicaoItem)
            .IsRequired(false)
            .HasMaxLength(10);

        builder.HasOne(ocp => ocp.OrdemDeCompra)
            .WithMany(oc => oc.Produtos)
            .HasForeignKey(ocp => ocp.OrdemDeCompraId);

        builder.ToTable(TabelasResources.OrdemCompraProduto);
    }
}

