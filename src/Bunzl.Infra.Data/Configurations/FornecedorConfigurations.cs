using Bunzl.Domain.Entities;
using Bunzl.Infra.CrossCutting.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bunzl.Infra.Data.Configurations;

public class FornecedorConfigurations : IEntityTypeConfiguration<Fornecedor>
{
    public void Configure(EntityTypeBuilder<Fornecedor> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.RazaoSocial)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(p => p.NomeFantasia)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(p => p.Logradouro)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Numero)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(p => p.ZipCode)
            .IsRequired(false)
            .HasMaxLength(9);

        builder.Property(p => p.Bairro)
           .HasMaxLength(100)
           .IsRequired(false);

        builder.Property(p => p.Pais)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.EstadoProvincia)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Cidade)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Contato)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(p => p.Website)
            .IsRequired(false)
            .HasMaxLength(200);

        builder.Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.TelefoneArea)
            .IsRequired(false)
            .HasMaxLength(5);

        builder.Property(p => p.Telefone)
            .IsRequired(false)
            .HasMaxLength(30);

        builder.Property(p => p.WhatsAppWechatArea)
            .IsRequired(false)
            .HasMaxLength(5);

        builder.Property(p => p.WhatsAppWechat)
            .IsRequired(false)
            .HasMaxLength(30);

        builder.Property(p => p.FlagJaEhParceiroBunzl)
            .IsRequired();

        builder.Property(p => p.FlagFabricaAuditadaBunzl)
            .IsRequired();

        builder.Property(p => p.FlagVeioDoERP) 
            .IsRequired();

        builder.Property(p => p.FabricasAuditadas)
            .IsRequired(false);

        builder.Property(p => p.QuaisTiposProdutosFabricam)
            .IsRequired(false);

        builder.Property(p => p.QuaisProdutosTercerizam)
            .IsRequired(false);

        builder.Property(p => p.QuaisProdutosMaisVendidos)
            .IsRequired(false);

        builder.Property(p => p.FazemOEM)
            .IsRequired(false);

        builder.Property(p => p.InformacoesGerais)
            .IsRequired(false);

        builder.Property(p => p.PossuemLaboratorioProprio)
            .IsRequired(false);

        builder.Property(p => p.InformacoesMercados)
            .IsRequired(false);

        builder.Property(p => p.InformacoesPoliticas)
            .IsRequired(false);

        builder.Property(p => p.NumeroIdentificacaoFiscal)
			.IsRequired(false)
            .HasMaxLength(100);

        builder.Property(p => p.CodigoERP)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(p => p.CodigoFornecedor)
	        .IsRequired(false)
	        .HasMaxLength(100);

		builder.Property(e => e.DataFabricaAuditada)
           .HasColumnType("timestamptz")
           .IsRequired(false);

        builder.Property(p => p.Status)
            .IsRequired();

        builder.Property(p => p.MoedaId)
            .IsRequired();

        builder.Property(p => p.QuantidadeAnosEmpresaMercado)
	        .IsRequired(false);

        builder.Property(p => p.ReceitaAnualEmpresa)
	        .IsRequired(false);

        builder.Property(p => p.QuantidadeTrabalhadoresEmpresaPossui)
	        .IsRequired(false);

        builder.Property(p => p.QuantidadeUnidadesFabricacaoOndeEstaoLocalizadas)
	        .IsRequired(false);

        builder.Property(p => p.CapacidadeProducaoFabricas)
	        .IsRequired(false);

        builder.Property(p => p.QuantidadeTurnosTrabalhoRealizados)
	        .IsRequired(false);

        builder.Property(p => p.EmpresaPossuiLaboratoriosPropriosParaPesquisaDesenvolvimento)
	        .IsRequired(false);

        builder.Property(p => p.QuaisMercadosEmpresaOpera)
	        .IsRequired(false);

        builder.Property(p => p.PorcentagemVendasMercadosRepresentam)
	        .IsRequired(false);

        builder.Property(p => p.PrincipaisClientesSegmentosAtendidosEmpresa)
	        .IsRequired(false);

        builder.Property(p => p.EmpresaPossuiClientesNoBrasilQuemSao)
	        .IsRequired(false);

        builder.Property(p => p.EmpresaOfereceExclusividadeProdutosRegioes)
	        .IsRequired(false);

        builder.Property(p => p.QuaisTermosPagamentosOferecidos)
	        .IsRequired(false);

		builder.Property(p => p.EmpresaPossuiCertificacaoFabricas)
            .IsRequired(false);

        builder.Property(p => p.FlagBloqueadoErp)
            .HasDefaultValue(false);

        builder.Property(p => p.CodigoTabelaPreco)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(p => p.SiglaFornecedor)
            .IsRequired(false)
            .HasMaxLength(5);

		builder
            .HasOne(f => f.FornecedorDadoBancario)
            .WithOne(fdb => fdb.Fornecedor)
            .HasForeignKey<FornecedorDadoBancario>(fdb => fdb.Id)
            .IsRequired(false);

        builder
            .HasMany(f => f.FornecedorDocumentos)
            .WithOne(fd => fd.Fornecedor)
            .HasForeignKey(fd => fd.FornecedorId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.FornecedorProdutos)
            .WithOne(fd => fd.Fornecedor)
            .HasForeignKey(fd => fd.FornecedorId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.TabelasPreco)
            .WithOne(fd => fd.Fornecedor)
            .HasForeignKey(fd => fd.FornecedorId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(f => f.Empresas)
            .WithMany(e => e.Fornecedores)
            .UsingEntity<Dictionary<string, object>>(
                "Fornecedor_x_Empresa",
                j => j.HasOne<Empresa>()
                      .WithMany()
                      .HasForeignKey("EmpresaId")
                      .OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Fornecedor>()
                      .WithMany()
                      .HasForeignKey("FornecedorId")
                      .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.Property<Guid>("FornecedorId").HasColumnName("FornecedorId");
                    j.Property<Guid>("EmpresaId").HasColumnName("EmpresaId");
                    j.HasKey("FornecedorId", "EmpresaId"); 
        });

        builder.ToTable(TabelasResources.Fornecedor);
    }
}