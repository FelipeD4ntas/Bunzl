using Bunzl.Domain.Entities;
using Bunzl.Infra.CrossCutting.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bunzl.Infra.Data.Configurations;

public class FornecedorObservacaoConfigurations : IEntityTypeConfiguration<FornecedorObservacao>
{
    public void Configure(EntityTypeBuilder<FornecedorObservacao> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Observacao)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(p => p.FornecedorId)
            .IsRequired();

        builder.ToTable(TabelasResources.FornecedorObservacao);
    }
}