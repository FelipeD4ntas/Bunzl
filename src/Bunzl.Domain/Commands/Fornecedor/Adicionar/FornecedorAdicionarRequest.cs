using Bunzl.Domain.DTOs;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.Adicionar;

public class FornecedorAdicionarRequest : IRequest<CommandResponse<FornecedorAdicionarResponse>>
{
    public required string RazaoSocial { get; set; }
    public required string NomeFantasia { get; set; }
    public required string Logradouro { get; set; }
    public required string Numero { get; set; }
    public string? ZipCode { get; set; }
    public string Bairro { get; set; }
    public required string Pais { get; set; }
    public required string EstadoProvincia { get; set; }
    public required string Cidade { get; set; }
    public string? Contato { get; set; }
    public string? Website { get; set; }
    public required string Email { get; set; }
    public string? TelefoneArea { get; set; }
    public string? Telefone { get; set; }
    public string? WhatsAppWechatArea { get; set; }
    public string? WhatsAppWechat { get; set; }
    public required bool FlagJaEhParceiroBunzl { get; set; }
    public required bool FlagFabricaAuditadaBunzl { get; set; }
    public string? FabricasAuditadas { get; set; }
    public string? QuaisTiposProdutosFabricam { get; set; }
    public string? QuaisProdutosTercerizam { get; set; }
    public string? QuaisProdutosMaisVendidos { get; set; }
    public string? FazemOEM { get; set; }
    public string? InformacoesGerais { get; set; }
    public int NumeroTrabalhadores { get; set; }
    public int NumeroUnidades { get; set; }
    public string? Localizacao { get; set; }
    public int Capacidade { get; set; }
    public int Turnos { get; set; }
    public string? Producao { get; set; }
    public string? PossuemLaboratorioProprio { get; set; }
    public string? InformacoesMercados { get; set; }
    public string? InformacoesPoliticas { get; set; }
    public string? NumeroIdentificacaoFiscal { get; set; }
    public string? CodigoERP { get; set; }
    public DateTime? DataFabricaAuditada { get; set; }
    public Guid MoedaId { get; set; }
    public string? QuantidadeAnosEmpresaMercado { get; set; }
    public string? ReceitaAnualEmpresa { get; set; }
    public string? QuantidadeTrabalhadoresEmpresaPossui { get; set; }
    public string? QuantidadeUnidadesFabricacaoOndeEstaoLocalizadas { get; set; }
    public string? CapacidadeProducaoFabricas { get; set; }
    public string? QuantidadeTurnosTrabalhoRealizados { get; set; }
    public string? EmpresaPossuiLaboratoriosPropriosParaPesquisaDesenvolvimento { get; set; }
    public string? QuaisMercadosEmpresaOpera { get; set; }
    public string? PorcentagemVendasMercadosRepresentam { get; set; }
    public string? PrincipaisClientesSegmentosAtendidosEmpresa { get; set; }
    public string? EmpresaPossuiClientesNoBrasilQuemSao { get; set; }
    public string? EmpresaOfereceExclusividadeProdutosRegioes { get; set; }
    public string? QuaisTermosPagamentosOferecidos { get; set; }
    public string? EmpresaPossuiCertificacaoFabricas { get; set; }
    public string? CodigoTabelaPreco { get; set; }
    public FornecedorDadoBancarioDto? FornecedorDadoBancario { get; set; }
}