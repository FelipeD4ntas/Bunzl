using Bunzl.Domain.Entities;
using Bunzl.Infra.CrossCutting.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bunzl.Infra.Data.Configurations;

public class OrdemDeCompraAnexoConfiguration : IEntityTypeConfiguration<OrdemDeCompraAnexo>
{
    public void Configure(EntityTypeBuilder<OrdemDeCompraAnexo> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Nome)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(o => o.Tipo)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(o => o.DataCriacao)
            .IsRequired();

        builder.Property(o => o.OrdemDeCompraId)
            .IsRequired();

        builder.HasOne(o => o.OrdemDeCompra)
            .WithMany(o => o.Anexos)
            .HasForeignKey(o => o.OrdemDeCompraId);

        builder.ToTable(TabelasResources.OrdemCompraAnexo);
    }
}

