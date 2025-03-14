using Bunzl.Domain.Entities;
using Bunzl.Infra.CrossCutting.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bunzl.Infra.Data.Configurations;

public class TabelaPrecoProdutoConfiguration : IEntityTypeConfiguration<TabelaPrecoProduto>
{
    public void Configure(EntityTypeBuilder<TabelaPrecoProduto> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.TabelaPrecoId)
            .IsRequired();

        builder.Property(p => p.ProdutoId)
            .IsRequired();

        builder.Property(p => p.UltimoPrecoPraticado)
            .HasColumnType("decimal(18,5)")
            .IsRequired();

        builder.Property(p => p.NovoPreco)
            .HasColumnType("decimal(18,6)")
            .IsRequired();

        builder.Property(p => p.Status)
            .IsRequired();

        builder.ToTable(TabelasResources.TabelaPrecoProduto);
    }
}
