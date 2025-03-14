using Bunzl.Domain.Entities;
using Bunzl.Infra.CrossCutting.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bunzl.Infra.Data.Configurations;

public class TabelaPrecoConfiguration : IEntityTypeConfiguration<TabelaPreco>
{
    public void Configure(EntityTypeBuilder<TabelaPreco> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.EmpresaId)
            .IsRequired();

        builder.Property(p => p.FornecedorId)
            .IsRequired();

        builder.Property(p => p.DataInicioVigencia)
            .IsRequired();

        builder.Property(p => p.DataFimVigencia)
			.IsRequired();

        builder.Property(p => p.CodigoERP)
			.HasMaxLength(20)
			.IsRequired(false);

        builder.Property(p => p.Status)
            .IsRequired();

        builder.Property(p => p.FlagExpirada)
            .IsRequired();

        builder.ToTable(TabelasResources.TabelaPreco);
    }
}
