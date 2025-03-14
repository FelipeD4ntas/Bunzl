using Bunzl.Domain.Entities;
using Bunzl.Infra.CrossCutting.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bunzl.Infra.Data.Configurations;

public class MoedaConfigurations : IEntityTypeConfiguration<Moeda>
{
    public void Configure(EntityTypeBuilder<Moeda> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Sigla)
            .IsRequired()
            .HasMaxLength(5);

        builder.Property(p => p.Descricao)
            .IsRequired()
            .HasMaxLength(100);

        builder.ToTable(TabelasResources.Moeda);
    }
}