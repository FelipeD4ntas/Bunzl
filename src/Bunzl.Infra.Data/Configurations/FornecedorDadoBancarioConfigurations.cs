using Bunzl.Domain.Entities;
using Bunzl.Infra.CrossCutting.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bunzl.Infra.Data.Configurations;

public class FornecedorDadoBancarioConfigurations : IEntityTypeConfiguration<FornecedorDadoBancario>
{
    public void Configure(EntityTypeBuilder<FornecedorDadoBancario> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .IsRequired()
            .HasColumnName("FornecedorId");

        builder.Property(p => p.NomeBanco)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(p => p.NumeroBanco)
            .HasMaxLength(30)
            .IsRequired(false);

        builder.Property(p => p.Agencia)
            .HasMaxLength(30)
            .IsRequired(false);

        builder.Property(p => p.NumeroContaCorrente)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(p => p.NomeBeneficiario)
            .IsRequired(false)
            .HasMaxLength(200);

        builder.Property(p => p.Logradouro)
            .HasMaxLength(200)
            .IsRequired(false);

        builder.Property(p => p.Numero)
            .HasMaxLength(20)
            .IsRequired(false);

        builder.Property(p => p.ZipCode)
            .HasMaxLength(9)
            .IsRequired(false);

        builder.Property(p => p.Bairro)
           .HasMaxLength(100)
           .IsRequired(false);

        builder.Property(p => p.Pais)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(p => p.EstadoProvincia)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(p => p.Cidade)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(p => p.Swift)
            .HasMaxLength(30)
            .IsRequired(false);

        builder.Property(p => p.Observacao)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(p => p.Iban)
            .HasMaxLength(30)
            .IsRequired(false);

        builder.Property(p => p.VatNumber)
            .HasMaxLength(30)
            .IsRequired(false);

        builder.ToTable(TabelasResources.FornecedorDadoBancario);
    }
}