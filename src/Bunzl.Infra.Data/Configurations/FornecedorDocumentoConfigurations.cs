using Bunzl.Domain.Entities;
using Bunzl.Infra.CrossCutting.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bunzl.Infra.Data.Configurations;

public class FornecedorDocumentoConfigurations : IEntityTypeConfiguration<FornecedorDocumento>
{
    public void Configure(EntityTypeBuilder<FornecedorDocumento> builder)
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

        builder.Property(p => p.Observacao)
            .IsRequired(false)
            .HasMaxLength(500);

        builder.Property(p => p.FornecedorId)
           .IsRequired();

        builder.HasOne(d => d.Fornecedor)
           .WithMany(f => f.FornecedorDocumentos)
           .HasForeignKey(d => d.FornecedorId)
           .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(TabelasResources.FornecedorDocumento);
    }
}