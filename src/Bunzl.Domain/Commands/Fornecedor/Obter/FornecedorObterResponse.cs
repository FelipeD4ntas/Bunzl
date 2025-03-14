using Bunzl.Domain.DTOs;
using Bunzl.Domain.Enumerators;

namespace Bunzl.Domain.Commands.Fornecedor.Obter;

public class FornecedorObterResponse   
(
    string codigoFornecedor,
    string razaoSocial,
    string nomeFantasia,
    string logradouro,
    string numero,
    string zipCode,
    string bairro,
    string pais,
    string estadoProvincia,
    string cidade,
    string contato,
    string? website,
    string email,
    string? telefoneArea,
    string? telefone,
    string? whatsAppWechatArea,
    string? whatsAppWechat,
    bool flagJaEhParceiroBunzl,
    bool flagFabricaAuditadaBunzl,
    bool flagHomologado,
    string? fabricasAuditadas,
    string? quaisTiposProdutosFabricam,
    string? quaisProdutosTercerizam,
    string? quaisProdutosMaisVendidos,
    string? fazemOEM,
    string? informacoesGerais,
    string? possuemLaboratorioProprio,
    string? informacoesMercados,
    string? informacoesPoliticas,
    string? numeroIdentificacaoFiscal,
    string? codigoERP,
    DateTime? dataFabricaAuditada,
    DateTime? dataAlteracao,
    EStatusFornecedor status,
    Guid moedaId,
    string? quantidadeAnosEmpresaMercado,
    string? receitaAnualEmpresa,
    string? quantidadeTrabalhadoresEmpresaPossui,
    string? quantidadeUnidadesFabricacaoOndeEstaoLocalizadas,
    string? capacidadeProducaoFabricas,
    string? quantidadeTurnosTrabalhoRealizados,
    string? empresaPossuiLaboratoriosPropriosParaPesquisaDesenvolvimento,
    string? quaisMercadosEmpresaOpera,
    string? porcentagemVendasMercadosRepresentam,
    string? principaisClientesSegmentosAtendidosEmpresa,
    string? empresaPossuiClientesNoBrasilQuemSao,
    string? empresaOfereceExclusividadeProdutosRegioes,
    string? quaisTermosPagamentosOferecidos,
    string? empresaPossuiCertificacaoFabricas,
    bool bloqueadoErp,
    string? codigoTabelaPreco,
    string? siglaFornecedor,
    FornecedorDadoBancarioDto? fornecedorDadoBancario,
    List<FornecedorDocumentoDto> fornecedorDocumentos
)
{
    public string CodigoFornecedor { get; set; } = codigoFornecedor;
    public string RazaoSocial { get; set; } = razaoSocial;
    public string NomeFantasia { get; set; } = nomeFantasia;
    public string Logradouro { get; set; } = logradouro;
    public string Numero { get; set; } = numero;
    public string? ZipCode { get; set; } = zipCode;
    public string? Bairro { get; set; } = bairro;
    public string Pais { get; set; } = pais;
    public string EstadoProvincia { get; set; } = estadoProvincia;
    public string Cidade { get; set; } = cidade;
    public string? Contato { get; set; } = contato;
    public string? Website { get; set; } = website;
    public string Email { get; set; } = email;
    public string? TelefoneArea { get; set; } = telefoneArea;
    public string? Telefone { get; set; } = telefone;
    public string? WhatsAppWechatArea { get; set; } = whatsAppWechatArea;
    public string? WhatsAppWechat { get; set; } = whatsAppWechat;
    public bool FlagJaEhParceiroBunzl { get; set; } = flagJaEhParceiroBunzl;
    public bool FlagFabricaAuditadaBunzl { get; set; } = flagFabricaAuditadaBunzl;
    public bool FlagHomologado { get; set; } = flagHomologado;
    public string? FabricasAuditadas { get; set; } = fabricasAuditadas;
    public string? QuaisTiposProdutosFabricam { get; set; } = quaisTiposProdutosFabricam;
    public string? QuaisProdutosTercerizam { get; set; } = quaisProdutosTercerizam;
    public string? QuaisProdutosMaisVendidos { get; set; } = quaisProdutosMaisVendidos;
    public string? FazemOEM { get; set; } = fazemOEM;
    public string? InformacoesGerais { get; set; } = informacoesGerais;
    public string? PossuemLaboratorioProprio { get; set; } = possuemLaboratorioProprio;
    public string? InformacoesMercados { get; set; } = informacoesMercados;
    public string? InformacoesPoliticas { get; set; } = informacoesPoliticas;
    public string? NumeroIdentificacaoFiscal { get; set; } = numeroIdentificacaoFiscal;
    public string? CodigoERP { get; set; } = codigoERP;
    public DateTime? DataFabricaAuditada { get; set; } = dataFabricaAuditada;
    public DateTime? DataAlteracao { get; set; } = dataAlteracao;
    public EStatusFornecedor Status { get; set; } = status;
    public Guid MoedaId { get; set; } = moedaId;
    public string? QuantidadeAnosEmpresaMercado { get; set; } = quantidadeAnosEmpresaMercado;
    public string? ReceitaAnualEmpresa { get; set; } = receitaAnualEmpresa;
    public string? QuantidadeTrabalhadoresEmpresaPossui { get; set; } = quantidadeTrabalhadoresEmpresaPossui;
    public string? QuantidadeUnidadesFabricacaoOndeEstaoLocalizadas { get; set; } = quantidadeUnidadesFabricacaoOndeEstaoLocalizadas;
    public string? CapacidadeProducaoFabricas { get; set; } = capacidadeProducaoFabricas;
    public string? QuantidadeTurnosTrabalhoRealizados { get; set; } = quantidadeTurnosTrabalhoRealizados;
    public string? EmpresaPossuiLaboratoriosPropriosParaPesquisaDesenvolvimento { get; set; } = empresaPossuiLaboratoriosPropriosParaPesquisaDesenvolvimento;
    public string? QuaisMercadosEmpresaOpera { get; set; } = quaisMercadosEmpresaOpera;
    public string? PorcentagemVendasMercadosRepresentam { get; set; } = porcentagemVendasMercadosRepresentam;
    public string? PrincipaisClientesSegmentosAtendidosEmpresa { get; set; } = principaisClientesSegmentosAtendidosEmpresa;
    public string? EmpresaPossuiClientesNoBrasilQuemSao { get; set; } = empresaPossuiClientesNoBrasilQuemSao;
    public string? EmpresaOfereceExclusividadeProdutosRegioes { get; set; } = empresaOfereceExclusividadeProdutosRegioes;
    public string? QuaisTermosPagamentosOferecidos { get; set; } = quaisTermosPagamentosOferecidos;
    public string? EmpresaPossuiCertificacaoFabricas { get; set; } = empresaPossuiCertificacaoFabricas;
    public bool FlagBloqueadoErp { get; set; } = bloqueadoErp;
    public string? CodigoTabelaPreco { get; set; } = codigoTabelaPreco;
    public string? SiglaFornecedor { get; set; } = siglaFornecedor;
    public virtual FornecedorDadoBancarioDto? FornecedorDadoBancario { get; set; } = fornecedorDadoBancario;
    public virtual List<FornecedorDocumentoDto> FornecedorDocumentos { get; set; } = fornecedorDocumentos;
}
