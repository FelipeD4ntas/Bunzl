using Microsoft.EntityFrameworkCore;
using Bunzl.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bunzl.Infra.CrossCutting.Resources;

namespace Bunzl.Infra.Data.Configurations;

public class NegociacaoComercialObservacaoConfiguration : IEntityTypeConfiguration<NegociacaoComercialObservacao>
{
	public void Configure(EntityTypeBuilder<NegociacaoComercialObservacao> builder)
	{
		builder.HasKey(p => p.Id);

		builder.Property(p => p.Observacao)
			.IsRequired()
			.HasMaxLength(500);

		builder.Property(p => p.NegociacaoComercialId)
			.IsRequired();

		builder.ToTable(TabelasResources.NegociacaoComercialObservacao);
	}
}

