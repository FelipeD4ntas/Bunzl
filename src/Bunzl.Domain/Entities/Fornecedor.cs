using Bunzl.Core.Domain.Entities.Base;
using Bunzl.Core.Domain.Interfaces.Base;
using Bunzl.Domain.Entities.Validations;
using Bunzl.Domain.Enumerators;

namespace Bunzl.Domain.Entities;

public class Fornecedor : EntityBase<Guid>, IAggregationRoot
{
    public string RazaoSocial { get; set; } = string.Empty;
    public string NomeFantasia { get; set; } = string.Empty;
    public string Logradouro { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string? ZipCode { get; set; }
    public string? Bairro { get; set; } = string.Empty;
    public string Pais { get; set; } = string.Empty;
    public string EstadoProvincia { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string? Contato { get; set; }
    public string? Website { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? TelefoneArea { get; set; }
    public string? Telefone { get; set; }
    public string? WhatsAppWechatArea { get; set; } = string.Empty;
    public string? WhatsAppWechat { get; set; } = string.Empty;
    public bool FlagJaEhParceiroBunzl { get; set; } = false;
    public bool FlagFabricaAuditadaBunzl { get; set; } = false;
    public bool FlagVeioDoERP { get; set; } = false;
    public string? FabricasAuditadas { get; set; }
    public string? QuaisTiposProdutosFabricam { get; set; }
    public string? QuaisProdutosTercerizam { get; set; }
    public string? QuaisProdutosMaisVendidos { get; set; }
    public string? FazemOEM { get; set; } 
    public string? InformacoesGerais { get; set; }
    public string? PossuemLaboratorioProprio { get; set; }
    public string? InformacoesMercados { get; set; }
    public string? InformacoesPoliticas { get; set; }
    public string? NumeroIdentificacaoFiscal { get; set; }
    public string? CodigoERP { get; set; }
    public string? CodigoFornecedor { get; set; }
    public DateTime? DataFabricaAuditada { get; set; }
    public EStatusFornecedor Status { get; set; } = EStatusFornecedor.AguardandoAprovacao;
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
    public bool FlagAtivo { get; set; } = true;
    public bool FlagBloqueadoErp { get; set; } = false;
    public string? CodigoTabelaPreco { get; set; }
    public string? SiglaFornecedor { get; set; }
    public virtual FornecedorDadoBancario? FornecedorDadoBancario { get; set; }
    public virtual List<FornecedorDocumento> FornecedorDocumentos { get; set; } = [];
    public virtual List<FornecedorObservacao> FornecedorObservacoes { get; set; } = [];
    public virtual List<FornecedorProduto> FornecedorProdutos { get; set; } = [];
    public virtual List<TabelaPreco> TabelasPreco { get; set; } = [];
    public List<Usuario> Usuarios { get; set; } = [];
    public List<Empresa> Empresas { get; set; } = [];
    public virtual Moeda Moeda { get; set; }

    public void AdicionarDadoBancario(FornecedorDadoBancario fornecedorDadoBancario)
    {
        FornecedorDadoBancario = fornecedorDadoBancario;
    }

    public void AdicionarDocumento(FornecedorDocumento fornecedorDocumento)
    {
        FornecedorDocumentos.Add(fornecedorDocumento);
    }

    public void AdicionarObservacao(FornecedorObservacao fornecedorObservacao)
    {
        FornecedorObservacoes.Add(fornecedorObservacao);
    }

    public void AdicionarProduto(FornecedorProduto fornecedorProduto)
    {
        FornecedorProdutos.Add(fornecedorProduto);
    }

    public void AdicionarTabelaPreco(TabelaPreco tabelaPreco)
    {
        TabelasPreco.Add(tabelaPreco);
    }

    public void DeletarDocumento(FornecedorDocumento fornecedorDocumento)
    {
        FornecedorDocumentos.Remove(fornecedorDocumento);
    }

    public void DeletarObservacao(FornecedorObservacao fornecedorObservacao)
    {
        FornecedorObservacoes.Remove(fornecedorObservacao);
    }

    public void DeletarProduto(FornecedorProduto fornecedorProduto)
    {
        FornecedorProdutos.Remove(fornecedorProduto);
    }

    public void RelacionarUsuario(Usuario usuario)
    {
        Usuarios.Add(usuario);
    }

    public void RelacionarUsuario(List<Usuario> usuarios)
    {
        usuarios.ForEach(RelacionarUsuario);
    }

    public void RelacionarEmpresa(Empresa empresa)
    {
        Empresas.Add(empresa);
    }

    public void Atualizar(Fornecedor fornecedorAtualizado)
    {
        CodigoFornecedor = fornecedorAtualizado.CodigoFornecedor;
        RazaoSocial = fornecedorAtualizado.RazaoSocial;
        NomeFantasia = fornecedorAtualizado.NomeFantasia;
        Logradouro = fornecedorAtualizado.Logradouro;
        Numero = fornecedorAtualizado.Numero;
        ZipCode = fornecedorAtualizado.ZipCode;
        Bairro = fornecedorAtualizado.Bairro;
        Pais = fornecedorAtualizado.Pais;
        EstadoProvincia = fornecedorAtualizado.EstadoProvincia;
        Cidade = fornecedorAtualizado.Cidade;
        Contato = fornecedorAtualizado.Contato;
        Website = fornecedorAtualizado.Website;
        Email = fornecedorAtualizado.Email;
        TelefoneArea = fornecedorAtualizado.TelefoneArea;
        Telefone = fornecedorAtualizado.Telefone;
        WhatsAppWechatArea = fornecedorAtualizado.WhatsAppWechatArea;
        WhatsAppWechat = fornecedorAtualizado.WhatsAppWechat;
        FlagJaEhParceiroBunzl = fornecedorAtualizado.FlagJaEhParceiroBunzl;
        FlagFabricaAuditadaBunzl = fornecedorAtualizado.FlagFabricaAuditadaBunzl;
        FabricasAuditadas = fornecedorAtualizado.FabricasAuditadas;
        QuaisTiposProdutosFabricam = fornecedorAtualizado.QuaisTiposProdutosFabricam;
        QuaisProdutosTercerizam = fornecedorAtualizado.QuaisProdutosTercerizam;
        QuaisProdutosMaisVendidos = fornecedorAtualizado.QuaisProdutosMaisVendidos;
        FazemOEM = fornecedorAtualizado.FazemOEM;
        InformacoesGerais = fornecedorAtualizado.InformacoesGerais;
        PossuemLaboratorioProprio = fornecedorAtualizado.PossuemLaboratorioProprio;
        InformacoesMercados = fornecedorAtualizado.InformacoesMercados;
        InformacoesPoliticas = fornecedorAtualizado.InformacoesPoliticas;
        Usuarios = fornecedorAtualizado.Usuarios;
        Empresas = fornecedorAtualizado.Empresas;
        NumeroIdentificacaoFiscal = fornecedorAtualizado.NumeroIdentificacaoFiscal;
		CodigoERP = fornecedorAtualizado.CodigoERP;
        DataFabricaAuditada = fornecedorAtualizado.DataFabricaAuditada;
        Status = fornecedorAtualizado.Status;
        MoedaId = fornecedorAtualizado.MoedaId;
        QuantidadeAnosEmpresaMercado = fornecedorAtualizado.QuantidadeAnosEmpresaMercado;
        ReceitaAnualEmpresa = fornecedorAtualizado.ReceitaAnualEmpresa;
        QuantidadeTrabalhadoresEmpresaPossui = fornecedorAtualizado.QuantidadeTrabalhadoresEmpresaPossui;
        QuantidadeUnidadesFabricacaoOndeEstaoLocalizadas = fornecedorAtualizado.QuantidadeUnidadesFabricacaoOndeEstaoLocalizadas;
        CapacidadeProducaoFabricas = fornecedorAtualizado.CapacidadeProducaoFabricas;
        QuantidadeTurnosTrabalhoRealizados = fornecedorAtualizado.QuantidadeTurnosTrabalhoRealizados;
        EmpresaPossuiLaboratoriosPropriosParaPesquisaDesenvolvimento = fornecedorAtualizado.EmpresaPossuiLaboratoriosPropriosParaPesquisaDesenvolvimento;
        QuaisMercadosEmpresaOpera = fornecedorAtualizado.QuaisMercadosEmpresaOpera;
        PorcentagemVendasMercadosRepresentam = fornecedorAtualizado.PorcentagemVendasMercadosRepresentam;
        PrincipaisClientesSegmentosAtendidosEmpresa = fornecedorAtualizado.PrincipaisClientesSegmentosAtendidosEmpresa;
        EmpresaPossuiClientesNoBrasilQuemSao = fornecedorAtualizado.EmpresaPossuiClientesNoBrasilQuemSao;
        EmpresaOfereceExclusividadeProdutosRegioes = fornecedorAtualizado.EmpresaOfereceExclusividadeProdutosRegioes;
        QuaisTermosPagamentosOferecidos = fornecedorAtualizado.QuaisTermosPagamentosOferecidos;
        EmpresaPossuiCertificacaoFabricas = fornecedorAtualizado.EmpresaPossuiCertificacaoFabricas;
        CodigoTabelaPreco = fornecedorAtualizado.CodigoTabelaPreco;
        SiglaFornecedor = fornecedorAtualizado.SiglaFornecedor;

        AddNotifications(new FornecedorValidator().Validate(this));
    }

    /*
     *
     * NumeroIdentificacaoFiscal = fornecedorExterno.CodigoPessoa,
       NomeFantasia = fornecedorExterno.NomeFantasia ?? "",
       RazaoSocial = fornecedorExterno.NomeCompleto ?? "",
       Logradouro = fornecedorExterno.Endereco ?? "",
       Cidade = fornecedorExterno.Cidade ?? "",
       EstadoProvincia = fornecedorExterno.Estado ?? "",
       Pais = fornecedorExterno.Pais ?? "",
       Telefone = fornecedorExterno.Telefone,
       Status = EStatusFornecedor.AguardandoAprovacao,
       CodigoERP = fornecedorExterno.CodigoERP,
       CodigoFornecedor = fornecedorExterno.CodigoFornecedor,
       FlagVeioDoERP = true,
       FlagBloqueadoErp = fornecedorExterno.IsBloqueado,
       MoedaId = moeda.First().Id
     *
     *
     *
     */

    public void AtualizarComDadosDoErp(
        string? codigoFornecedor,
        string? codigoERP,
        string? pais,
        string? nomeCompleto,
        string? codigoNF,
        string? nomeFantasia,
        string? endereco,
        string? numero,
        string? bairro,
        string? estado,
        string? zipCode,
        string? cidade,
        string? website,
        string? contato,
        string? telefoneArea,
        string? telefone,
        string? whatsAppWechatArea,
        string? whatsAppWechat,
        string? siglaFornecedor,
        bool isBloqueado)
    {
        CodigoFornecedor = codigoFornecedor;
        CodigoERP = codigoERP;
        Pais = pais ?? "";
        RazaoSocial = nomeCompleto ?? "";
        NumeroIdentificacaoFiscal = codigoNF;
        NomeFantasia = nomeFantasia ?? "";
        Logradouro = endereco ?? "";
        Numero = numero ?? "";
        Bairro = bairro;
        EstadoProvincia = estado ?? "";
        ZipCode = zipCode;
        Cidade = cidade ?? "";
        Website = website;
        Contato = contato;
        TelefoneArea = telefoneArea;
        Telefone = telefone;
        WhatsAppWechatArea = whatsAppWechatArea;
        WhatsAppWechat = whatsAppWechat;
        SiglaFornecedor = siglaFornecedor;
        FlagBloqueadoErp = isBloqueado;
        FlagVeioDoERP = true;

        AddNotifications(new FornecedorValidator().Validate(this));
    }

    public void AtualizarDadoBancario(FornecedorDadoBancario fornecedorDadoBancario)
    {
        FornecedorDadoBancario = fornecedorDadoBancario;
    }

    public void Ativar()
    {
        FlagAtivo = true;
        Status = EStatusFornecedor.NaoHomologado;
    }

    public void Inativar()
    {
        FlagAtivo = false;
        Status = EStatusFornecedor.Inativo;
    }

    public void InvalidarPortal()
    {
        FlagAtivo = false;
        Status = EStatusFornecedor.InvalidoPortal;
    }
}