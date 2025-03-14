using Bunzl.Domain.Entities;
using Bunzl.Infra.CrossCutting.Resources;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Bunzl.Infra.Data.Configurations;

public class OrdemDeCompraObservacaoConfiguration : IEntityTypeConfiguration<OrdemDeCompraObservacao>
{
    public void Configure(EntityTypeBuilder<OrdemDeCompraObservacao> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Observacao)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasOne(o => o.OrdemDeCompra)
            .WithMany(o => o.Observacoes)
            .HasForeignKey(o => o.OrdemDeCompraId);

        builder.ToTable(TabelasResources.OrdemCompraObservacao);
    }
}