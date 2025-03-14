using Bunzl.Domain.Entities;
using Bunzl.Infra.CrossCutting.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bunzl.Infra.Data.Configurations;

public class FornecedorProdutoAnexoConfiguration : IEntityTypeConfiguration<FornecedorProdutoAnexo>
{
    public void Configure(EntityTypeBuilder<FornecedorProdutoAnexo> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .IsRequired()
            .HasColumnName("Id");

        builder.Property(p => p.Nome)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(p => p.Tipo)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.TipoDocumento)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Observacao)
            .IsRequired(false)
            .HasMaxLength(500);

        builder.Property(p => p.FornecedorProdutoId)
           .IsRequired();

        builder.HasOne(d => d.FornecedorProduto)
           .WithMany(f => f.FornecedorProdutoAnexos)
           .HasForeignKey(d => d.FornecedorProdutoId)
           .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(TabelasResources.FornecedorProdutoAnexo);
    }
}
