using Microsoft.EntityFrameworkCore;
using Bunzl.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bunzl.Infra.CrossCutting.Resources;

namespace Bunzl.Infra.Data.Configurations;

public class OrdemDeCompraConfiguration : IEntityTypeConfiguration<OrdemDeCompra>
{
    public void Configure(EntityTypeBuilder<OrdemDeCompra> builder)
    {
        builder.HasKey(oc => oc.Id);

        builder.Property(nc => nc.FornecedorId)
            .IsRequired();

        builder.Property(nc => nc.EmpresaId)
            .IsRequired();

        builder.Property(oc => oc.CodigoFornecedor)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(oc => oc.CodigoErpFornecedor)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(oc => oc.PaisImportador)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(oc => oc.NumeroOrdem)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(oc => oc.DataOrdem)
            .IsRequired(false);

        builder.Property(oc => oc.NumeroRevisao)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(oc => oc.DataRevisao)
            .IsRequired(false);

        builder.Property(oc => oc.CodigoEstabelecimento)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(oc => oc.DataExp)
            .IsRequired(false);

        builder.Property(oc => oc.NomeFornecedor)
            .IsRequired(false)
            .HasMaxLength(200);

        builder.Property(oc => oc.EnderecoFornecedor)
            .IsRequired(false)
            .HasMaxLength(500);

        builder.Property(oc => oc.NumeroEnderecoFornecedor)
            .IsRequired(false)
            .HasMaxLength(20);

        builder.Property(oc => oc.ContatoFornecedor)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(oc => oc.EmailFornecedor)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(oc => oc.CodigoFabricante)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(oc => oc.NomeFabricante)
            .IsRequired(false)
            .HasMaxLength(200);

        builder.Property(oc => oc.EnderecoFabricante)
            .IsRequired(false)
            .HasMaxLength(500);

        builder.Property(oc => oc.NomeImportador)
            .IsRequired(false)
            .HasMaxLength(200);

        builder.Property(oc => oc.EnderecoImportador)
            .IsRequired(false)
            .HasMaxLength(500);

        builder.Property(oc => oc.ContatoImportador)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(oc => oc.EmailImportador)
            .IsRequired(false)
            .HasMaxLength(200);

        builder.Property(oc => oc.NumeroEnderecoImportador)
            .IsRequired(false)
            .HasMaxLength(20);

        builder.Property(oc => oc.ComplementoEnderecoImportador)
            .IsRequired(false)
            .HasMaxLength(200);

        builder.Property(oc => oc.BairroEnderecoImportador)
            .IsRequired(false)
            .HasMaxLength(200);

        builder.Property(oc => oc.EstadoProvinciaImportador)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(oc => oc.ZipCodeImportador)
            .IsRequired(false)
            .HasMaxLength(50);

        builder.Property(oc => oc.CnpjImportador)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(oc => oc.PrazoPagamento)
            .IsRequired(false);

        builder.Property(oc => oc.TipoFrete)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(oc => oc.ModoEntrega)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(oc => oc.NomeAgente)
            .IsRequired(false)
            .HasMaxLength(200);

        builder.Property(oc => oc.Destino)
            .IsRequired(false)
            .HasMaxLength(200);

        builder.Property(oc => oc.NumeroContainer20)
            .IsRequired(false);

        builder.Property(oc => oc.NumeroContainer40)
            .IsRequired(false);

        builder.Property(oc => oc.NumeroContainer40HC)
            .IsRequired(false);

        builder.Property(oc => oc.NumeroContainerOutros)
            .IsRequired(false);

        builder.Property(oc => oc.TotalCBM)
            .IsRequired(false)
            .HasColumnType("decimal(18,2)");

        builder.Property(oc => oc.PesoTotal)
            .IsRequired(false)
            .HasColumnType("decimal(18,6)");

        builder.Property(oc => oc.NomeComprador)
            .IsRequired(false)
            .HasMaxLength(200);

        builder.Property(oc => oc.NomeVendedor)
            .IsRequired(false)
            .HasMaxLength(200);

        builder.Property(oc => oc.DataAlteracao)
            .IsRequired(false);

        builder.Property(oc => oc.TaxaEmbalagem)
            .IsRequired(false)
            .HasColumnType("decimal(18,2)");

        builder.Property(oc => oc.TaxaInterna)
            .IsRequired(false)
            .HasColumnType("decimal(18,2)");

        builder.Property(oc => oc.OutrasDespesas)
            .IsRequired(false)
            .HasColumnType("decimal(18,2)");

        builder.Property(oc => oc.Desconto)
            .IsRequired(false)
            .HasColumnType("decimal(18,2)");

        builder.Property(oc => oc.Frete)
            .IsRequired(false)
            .HasColumnType("decimal(18,2)");

        builder.Property(oc => oc.AcordoFornecimento)
            .IsRequired(false);

        builder.Property(oc => oc.ValorTotal)
            .IsRequired(false)
            .HasColumnType("decimal(18,6)");

        builder.Property(oc => oc.Status);

		builder.HasMany(oc => oc.Produtos)
            .WithOne(p => p.OrdemDeCompra)
            .HasForeignKey(p => p.OrdemDeCompraId);

        builder.HasMany(oc => oc.Anexos)
            .WithOne(p => p.OrdemDeCompra)
            .HasForeignKey(p => p.OrdemDeCompraId);

        builder.HasMany(oc => oc.Observacoes)
            .WithOne(p => p.OrdemDeCompra)
            .HasForeignKey(p => p.OrdemDeCompraId);

        builder.HasMany(oc => oc.UnidadesMedida)
            .WithOne(p => p.OrdemDeCompra)
            .HasForeignKey(p => p.OrdemDeCompraId);

        builder.ToTable(TabelasResources.OrdemCompra);
    }
}
