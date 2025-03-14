using Bunzl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bunzl.Infra.CrossCutting.Resources;

namespace Bunzl.Infra.Data.Configurations;

public class NegociacaoComercialAnexoConfiguration : IEntityTypeConfiguration<NegociacaoComercialAnexo>
{
    public void Configure(EntityTypeBuilder<NegociacaoComercialAnexo> builder)
    {
        builder.HasKey(nca => nca.Id);

        builder.Property(nca => nca.Nome)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(nca => nca.Tipo)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(nca => nca.Observacao)
            .HasMaxLength(500);

        builder.Property(nca => nca.NegociacaoComercialId)
            .IsRequired();

        builder.HasOne(nca => nca.NegociacaoComercial)
            .WithMany(nc => nc.Anexos)
            .HasForeignKey(nca => nca.NegociacaoComercialId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(TabelasResources.NegociacaoComercialAnexo);
    }
}

