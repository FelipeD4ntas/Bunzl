using Bunzl.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Bunzl.Infra.CrossCutting.Resources;

namespace Bunzl.Infra.Data.Configurations;

public class FornecedorProdutoConfiguration : IEntityTypeConfiguration<FornecedorProduto>
{
    public void Configure(EntityTypeBuilder<FornecedorProduto> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .IsRequired()
            .HasColumnName("Id");

        builder.Property(p => p.CodigoFornecedor)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.DescricaoCompletaFornecedor)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(p => p.DescricaoCompletaBunzl)
            .HasMaxLength(500);

        builder.Property(p => p.AplicacoesPrincipais)
            .HasMaxLength(500);

        builder.Property(p => p.Composicao)
            .IsRequired(false);

        builder.Property(p => p.Tamanho)
            .HasMaxLength(50);

        builder.Property(p => p.Cor)
            .HasMaxLength(50);

        builder.Property(p => p.CodigoNCM)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.UnidadeMedidaFornecedorMOQ)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.UnidadeMedidaFornecedorPreco)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.UnidadeMedidaBunzl)
            .HasMaxLength(50);

        builder.Property(p => p.IncotermId)
            .IsRequired(false);

        builder.Property(p => p.TermoPagamento)
            .HasMaxLength(300);

        builder.Property(p => p.Observacoes)
            .HasMaxLength(1000);

        builder.Property(p => p.DetalhesEmbalagem)
            .HasMaxLength(500);

        builder.Property(p => p.PortoEmbarque)
            .HasMaxLength(100);

        builder.Property(p => p.CodigoArtigo)
            .HasMaxLength(50);

        builder.Property(p => p.Familia)
            .HasMaxLength(50);

        builder.Property(p => p.CodigoSku)
            .HasMaxLength(50);

        builder.Property(p => p.CorBunzl)
            .HasMaxLength(50);

        builder.Property(p => p.TamanhoBunzl)
            .HasMaxLength(50);

        builder.Property(p => p.Preco)
            .HasColumnType("decimal(18,6)")
            .IsRequired();

        builder.Property(p => p.PesoBruto)
            .HasColumnType("decimal(18,5)");

        builder.Property(p => p.Comprimento)
            .HasColumnType("decimal(18,5)")
            .IsRequired();

        builder.Property(p => p.Largura)
            .HasColumnType("decimal(18,5)")
            .IsRequired();

        builder.Property(p => p.Altura)
            .HasColumnType("decimal(18,5)")
            .IsRequired();

        builder.Property(p => p.CBM)
            .HasColumnType("decimal(18,5)")
            .IsRequired();

        builder.Property(p => p.TempoEntrega)
            .IsRequired(false);

        builder.Property(p => p.CustoDesenvolvimentoEmbalagem)
            .HasColumnType("decimal(18,5)");

        builder.Property(p => p.CustoRotulagemEmbalagem)
            .HasColumnType("decimal(18,5)");

        builder.Property(p => p.QuantidadeCarregamentoContainer20Ft)
	        .HasColumnType("int");

        builder.Property(p => p.QuantidadeCarregamentoContainer40Ft)
	        .HasColumnType("int");

        builder.Property(p => p.QuantidadeCarregamentoContainer40Hc)
	        .HasColumnType("int");

		builder.Property(p => p.Status)
            .IsRequired();

        builder.Property(p => p.FornecedorId)
           .IsRequired();

        builder.Property(p => p.TipoEmbalagemInterna)
			.HasMaxLength(100);

        builder.Property(p => p.QuantidadePorEmbalagemInterna)
			.HasColumnType("int");

		builder.Property(p => p.TipoCaixaMaster)
			.HasMaxLength(100);

		builder.Property(p => p.QuantidadePorCaixaMaster)
			.HasColumnType("int");

		builder.Property(p => p.CapacidadeMensalFabrica)
		   .HasColumnType("decimal(18,5)");

		builder.Property(p => p.UnidadeMedidaCapacidadeMensal)
			.HasMaxLength(100);

		builder.Property(p => p.NomeFabrica)
			.HasMaxLength(100);

		builder.Property(p => p.CustoDetalhadoMateriaPrima)
			.HasColumnType("decimal(5, 2)");

		builder.Property(p => p.CustoDetalhadoCombustivel)
			.HasColumnType("decimal(5, 2)");

		builder.Property(p => p.CustoDetalhadoEmbalagem)
	        .HasColumnType("decimal(5, 2)");

		builder.Property(p => p.CustoDetalhadoMaoDeObra)
			.HasColumnType("decimal(5, 2)");

		builder.Property(p => p.CustoDetalhadoEnergia)
			.HasColumnType("decimal(5, 2)");

		builder.Property(p => p.CustoDetalhadoTransporte)
			.HasColumnType("decimal(5, 2)");

		builder
            .HasMany(f => f.FornecedorProdutoAnexos)
            .WithOne(fd => fd.FornecedorProduto)
            .HasForeignKey(fd => fd.FornecedorProdutoId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(TabelasResources.FornecedorProduto);
    }
}

