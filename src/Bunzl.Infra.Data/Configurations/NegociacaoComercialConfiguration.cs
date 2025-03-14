using Microsoft.EntityFrameworkCore;
using Bunzl.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bunzl.Infra.CrossCutting.Resources;

namespace Bunzl.Infra.Data.Configurations;

public class NegociacaoComercialConfiguration : IEntityTypeConfiguration<NegociacaoComercial>
{
    public void Configure(EntityTypeBuilder<NegociacaoComercial> builder)
    {
        builder.HasKey(nc => nc.Id);

        builder.Property(nc => nc.Codigo)
            .IsRequired();

        builder.Property(nc => nc.FornecedorId)
            .IsRequired();

        builder.Property(nc => nc.DataEntrega)
            .IsRequired();

        builder.Property(nc => nc.Titulo)
            .HasMaxLength(250);

        builder.Property(nc => nc.CampoAtuacao)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(nc => nc.TermosPagamento)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(nc => nc.Status)
            .IsRequired();

        builder.Property(nc => nc.EmpresaId)
            .IsRequired();

        builder.Property(nc => nc.ValorTotal)
	        .HasColumnType("decimal(18,2)")
	        .IsRequired();

		builder.HasMany(nc => nc.Produtos)
            .WithOne(p => p.NegociacaoComercial)
            .HasForeignKey(p => p.NegociacaoComercialId)
            .IsRequired();

        builder.HasMany(nc => nc.Anexos)
            .WithOne(a => a.NegociacaoComercial)
            .HasForeignKey(a => a.NegociacaoComercialId);

        builder.HasMany(nc => nc.NegociacaoComercialObservacoes)
            .WithOne(no => no.NegociacaoComercial)
			.HasForeignKey(no => no.NegociacaoComercialId);

        builder.ToTable(TabelasResources.NegociacaoComercial);
    }
}

