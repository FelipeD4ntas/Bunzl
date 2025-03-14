using Bunzl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bunzl.Infra.CrossCutting.Resources;

namespace Bunzl.Infra.Data.Configurations;

public class OrdemDeCompraUnidadeMedidaConfiguration : IEntityTypeConfiguration<OrdemDeCompraUnidadeMedida>
{
    public void Configure(EntityTypeBuilder<OrdemDeCompraUnidadeMedida> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.UnidadeMedida)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.QuantidadeTotal)
            .IsRequired()
            .HasColumnType("decimal(18,4)");

        builder.HasOne(o => o.OrdemDeCompra)
            .WithMany(o => o.UnidadesMedida)
            .HasForeignKey(o => o.OrdemDeCompraId);

        builder.ToTable(TabelasResources.OrdemCompraUnidadeMedida);
    }
}

