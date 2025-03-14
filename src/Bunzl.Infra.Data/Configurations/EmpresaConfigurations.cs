using Bunzl.Domain.Entities;
using Bunzl.Infra.CrossCutting.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bunzl.Infra.Data.Configurations;

public class EmpresaConfigurations : IEntityTypeConfiguration<Empresa>
{
    public void Configure(EntityTypeBuilder<Empresa> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Cnpj)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.EmpresaBunzlId)
            .HasMaxLength(50);

        builder.Property(e => e.DataUltimaAtualizacao)
            .HasColumnType("timestamptz")
            .IsRequired(false);

        builder.Property(e => e.DataUltimaAtualizacaoOrdemDeCompra)
	        .HasColumnType("timestamptz")
	        .IsRequired(false);

        builder.Property(e => e.FlagRegravarTabelaPrecoErp)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.SiglaEmpresa)
            .IsRequired(false)
            .HasMaxLength(10);

        builder.ToTable(TabelasResources.Empresa);
    }
}