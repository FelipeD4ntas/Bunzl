using Bunzl.Domain.DTOs;
using Bunzl.Domain.Enumerators;

namespace Bunzl.Domain.Commands.Fornecedor.Atualizar;

public class FornecedorAtualizarRequestPayload
{
    public string CodigoFornecedor { get; set; } = string.Empty;
    public string RazaoSocial { get; set; } = string.Empty;
    public string NomeFantasia { get; set; } = string.Empty;
    public string Logradouro { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string? ZipCode { get; set; }
    public string Bairro { get; set; } = string.Empty;
    public string Pais { get; set; } = string.Empty;
    public string EstadoProvincia { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string? Contato { get; set; }
    public string? Website { get; set; }
    public string Email { get; set; } = string.Empty;
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
    public EStatusFornecedor Status { get; set; }
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
    public string? SiglaFornecedor { get; set; }
	public FornecedorDadoBancarioDto? FornecedorDadoBancario { get; set; }

    public FornecedorAtualizarRequest ToRequest(Guid fornecedorId)
    {
        return new FornecedorAtualizarRequest(
            fornecedorId, 
            CodigoFornecedor,
            RazaoSocial, 
            NomeFantasia,
            Logradouro,
            Numero,
            ZipCode,
            Bairro,
            Pais,
            EstadoProvincia,
            Cidade,
            Contato,
            Website,
            Email,
            TelefoneArea,
            Telefone,
            WhatsAppWechatArea,
            WhatsAppWechat,
            FlagJaEhParceiroBunzl,
            FlagFabricaAuditadaBunzl,
            FabricasAuditadas,
            QuaisTiposProdutosFabricam,
            QuaisProdutosTercerizam,
            QuaisProdutosMaisVendidos,
            FazemOEM,
            InformacoesGerais,
            NumeroTrabalhadores,
            NumeroUnidades,
            Localizacao,
            Capacidade,
            Turnos,
            Producao,
            PossuemLaboratorioProprio,
            InformacoesMercados,
            InformacoesPoliticas,
            NumeroIdentificacaoFiscal,
            CodigoERP,
            DataFabricaAuditada,
            Status,
            MoedaId,
            QuantidadeAnosEmpresaMercado,
            ReceitaAnualEmpresa,
            QuantidadeTrabalhadoresEmpresaPossui,
            QuantidadeUnidadesFabricacaoOndeEstaoLocalizadas,
            CapacidadeProducaoFabricas,
            QuantidadeTurnosTrabalhoRealizados,
            EmpresaPossuiLaboratoriosPropriosParaPesquisaDesenvolvimento,
            QuaisMercadosEmpresaOpera,
            PorcentagemVendasMercadosRepresentam,
            PrincipaisClientesSegmentosAtendidosEmpresa,
            EmpresaPossuiClientesNoBrasilQuemSao,
            EmpresaOfereceExclusividadeProdutosRegioes,
            QuaisTermosPagamentosOferecidos,
            EmpresaPossuiCertificacaoFabricas,
            CodigoTabelaPreco,
            SiglaFornecedor,
            FornecedorDadoBancario
        );
    }
}
