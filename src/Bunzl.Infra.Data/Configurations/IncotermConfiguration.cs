using Bunzl.Domain.Entities;
using Bunzl.Infra.CrossCutting.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bunzl.Infra.Data.Configurations;

public class IncotermConfigurations : IEntityTypeConfiguration<Incoterm>
{
    public void Configure(EntityTypeBuilder<Incoterm> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Sigla)
            .IsRequired()
            .HasMaxLength(5);

        builder.Property(p => p.Descricao)
            .IsRequired()
            .HasMaxLength(100);

        builder.ToTable(TabelasResources.Incoterm);
    }
}