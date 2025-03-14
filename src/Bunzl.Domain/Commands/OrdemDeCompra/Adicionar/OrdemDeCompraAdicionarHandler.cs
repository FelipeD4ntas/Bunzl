using System.Text;
using Bunzl.Core.Domain.Interfaces.ExternalService;
using Bunzl.Domain.Entities;
using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.Helper;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using MediatR;

namespace Bunzl.Domain.Commands.OrdemDeCompra.Adicionar;

public class OrdemDeCompraAdicionarHandler(
    IPublisher mediator,
    IExternalServiceOrdemDeCompra externalServiceOrdemDeCompra,
    IRepositoryFornecedor repositoryFornecedor,
    IRepositoryUsuario repositoryUsuario,
    IRepositoryOrdemDeCompra repositoryOrdemDeCompra,
    IRepositoryEmpresa repositoryEmpresa,
    IUsuarioAutenticado usuarioAutenticado)
    : Notifiable, IRequestHandler<OrdemDeCompraAdicionarRequest, CommandResponse<OrdemDeCompraAdicionarResponse>>
{
    private StringBuilder AcordoFornecimentoFormatado = new StringBuilder();

    public async Task<CommandResponse<OrdemDeCompraAdicionarResponse>> Handle(OrdemDeCompraAdicionarRequest request, CancellationToken cancellationToken)
    {
        var empresa = await repositoryEmpresa.GetByAsync(false, e => e.Id == usuarioAutenticado.UsuarioEmpresa, false, cancellationToken);
        if (empresa is null)
        {
            AddNotification("OrdemDeCompra", EmpresaResources.EmpresaNaoEncontrada);
            return new CommandResponse<OrdemDeCompraAdicionarResponse>(this);
        }

        var ordensDeCompra = (await SafeExecutionHelper.SafeExecuteAsync(
            () => externalServiceOrdemDeCompra.ObterOrdemDeCompraPorDataInicioDataFim(empresa.Cnpj, request.DataInicio, request.DataFim, cancellationToken),
            AddNotification, "OrdemDeCompra", IntegracaoResources.CnpjEmpresaNaoEncontrouOrdemDeCompra)).ToList();

        //Verificando se todos os Fornecedores da Ordem de Compra do ERP estão casdastrados
        ordensDeCompra.ForEach(ordemDeCompraDto =>
        {
            var fornecedor = repositoryFornecedor.GetByAsync(false, f => f.CodigoFornecedor == ordemDeCompraDto.CodigoFornecedor, false, cancellationToken).Result;
            if (fornecedor is null)
                AddNotification("OrdemDeCompra", string.Format(FornecedorResources.FornecedorNaoEncontradoCodigo, string.IsNullOrEmpty(ordemDeCompraDto.CodigoFornecedor) ? ordemDeCompraDto.CodigoErpFornecedor : ordemDeCompraDto.CodigoFornecedor));
        });

        if (IsInvalid())
            return new CommandResponse<OrdemDeCompraAdicionarResponse>(this);

        foreach (var ordemDeCompraDto in ordensDeCompra)
        {
            var ordemDeCompraExistente = repositoryOrdemDeCompra.GetByAsync(true, o => o.NumeroOrdem == ordemDeCompraDto.NumeroOrdem && o.EmpresaId == empresa.Id, cancellationToken, p => p.Fornecedor).Result;
            if (ordemDeCompraExistente is null)
            {
                var fornecedor = repositoryFornecedor.GetByAsync(true, f => f.CodigoFornecedor == ordemDeCompraDto.CodigoFornecedor, false, cancellationToken).Result;
                var novaOrdemDeCompra = new Entities.OrdemDeCompra
                {
                    FornecedorId = fornecedor!.Id,
                    EmpresaId = empresa.Id,
                    CodigoFornecedor = ordemDeCompraDto.CodigoFornecedor,
                    CodigoErpFornecedor = ordemDeCompraDto.CodigoErpFornecedor,
                    PaisImportador = ordemDeCompraDto.PaisImportador,
                    NumeroOrdem = ordemDeCompraDto.NumeroOrdem,
                    DataOrdem = ordemDeCompraDto.DataOrdem?.ToUniversalTime(),
                    NumeroRevisao = ordemDeCompraDto.NumeroRevisao?.ToString(),
                    DataRevisao = ordemDeCompraDto.DataRevisao?.ToUniversalTime(),
                    CodigoEstabelecimento = ordemDeCompraDto.CodigoEstabelecimento,
                    DataExp = ordemDeCompraDto.DataExp?.ToUniversalTime(),
                    NomeFornecedor = ordemDeCompraDto.NomeFornecedor,
                    EnderecoFornecedor = ordemDeCompraDto.EnderecoFornecedor,
                    NumeroEnderecoFornecedor = ordemDeCompraDto.NumeroEnderecoFornecedor,
                    ContatoFornecedor = ordemDeCompraDto.ContatoFornecedor,
                    //EmailFornecedor = ordemDeCompraDto.EmailFornecedor,
                    CodigoFabricante = ordemDeCompraDto.CodigoFabricante,
                    NomeFabricante = ordemDeCompraDto.NomeFabricante,
                    EnderecoFabricante = ordemDeCompraDto.EnderecoFabricante,
                    NomeImportador = ordemDeCompraDto.NomeImportador,
                    EnderecoImportador = ordemDeCompraDto.EnderecoImportador,
                    ContatoImportador = ordemDeCompraDto.ContatoImportador,
                    EmailImportador = ordemDeCompraDto.EmailImportador,
                    NumeroEnderecoImportador = ordemDeCompraDto.NumeroEnderecoImportador,
                    ComplementoEnderecoImportador = ordemDeCompraDto.ComplementoEnderecoImportador,
                    BairroEnderecoImportador = ordemDeCompraDto.BairroEnderecoImportador,
                    EstadoProvinciaImportador = ordemDeCompraDto.EstadoProvinciaImportador,
                    ZipCodeImportador = ordemDeCompraDto.ZipCodeImportador,
                    CnpjImportador = ordemDeCompraDto.CnpjImportador,
                    PrazoPagamento = ordemDeCompraDto.PrazoPagamento,
                    TipoFrete = ordemDeCompraDto.TipoFrete,
                    ModoEntrega = ordemDeCompraDto.ModoEntrega,
                    NomeAgente = ordemDeCompraDto.NomeAgente,
                    Destino = ordemDeCompraDto.Destino,
                    NumeroContainer20 = ordemDeCompraDto.NumeroContainer20,
                    NumeroContainer40 = ordemDeCompraDto.NumeroContainer40,
                    NumeroContainer40HC = ordemDeCompraDto.NumeroContainer40HC,
                    NumeroContainerOutros = ordemDeCompraDto.NumeroContainerOutros,
                    TotalCBM = ordemDeCompraDto.TotalCBM,
                    PesoTotal = ordemDeCompraDto.PesoTotal,
                    NomeComprador = ordemDeCompraDto.NomeComprador,
                    NomeVendedor = ordemDeCompraDto.NomeVendedor,
                    DataAlteracao = ordemDeCompraDto.DataAlteracao?.ToUniversalTime(),
                    TaxaEmbalagem = ordemDeCompraDto.TaxaEmbalagem,
                    TaxaInterna = ordemDeCompraDto.TaxaInterna,
                    OutrasDespesas = ordemDeCompraDto.OutrasDespesas,
                    Desconto = ordemDeCompraDto.Desconto,
                    Frete = ordemDeCompraDto.Frete,
                    AcordoFornecimento = RetornarAcordoFornecimentoFormatado(ordemDeCompraDto.AcordoFornecimento),
                    Produtos = ordemDeCompraDto.Produtos.Select(produtoDto => new OrdemDeCompraProduto
                    {
                        OrdemItem = produtoDto.OrdemItem,
                        CodigoItem = produtoDto.CodigoItem,
                        CodigoSKU = produtoDto.Sku,
                        Descricao = produtoDto.Descricao,
                        UnidadeMedida = produtoDto.UnidadeMedida,
                        CodigoNCM = produtoDto.Ncm,
                        Quantidade = produtoDto.Quantidade,
                        MoedaSigla = produtoDto.Moeda,
                        ValorUnitario = produtoDto.ValorUnitario,
                        ValorTotal = produtoDto.ValorTotal,
                        DataEstimadaPartida = produtoDto.Etd?.AddDays(-7).ToUniversalTime(),
                        Etd = produtoDto.Etd?.ToUniversalTime(),
                        NumeroLote = produtoDto.NumeroLote,
                        PosicaoItem = produtoDto.PosicaoItem
                    }).ToList()
                };

                novaOrdemDeCompra.ValorTotal = (novaOrdemDeCompra.Produtos.Sum(produto => produto.ValorTotal) + novaOrdemDeCompra.OutrasDespesas + novaOrdemDeCompra.Frete + novaOrdemDeCompra.TaxaEmbalagem + novaOrdemDeCompra.TaxaInterna) - novaOrdemDeCompra.Desconto;
                novaOrdemDeCompra.UnidadesMedida = CalcularTotalPorUnidade(novaOrdemDeCompra.Produtos, novaOrdemDeCompra);

                await repositoryOrdemDeCompra.AddAsync(novaOrdemDeCompra, cancellationToken);
            }
            else
            {
                await repositoryOrdemDeCompra.RemoverProdutosPorOrdemDeCompraIdAsync(ordemDeCompraExistente.Id, cancellationToken);
                await repositoryOrdemDeCompra.RemoverUnidadesMedidaPorOrdemDeCompraIdAsync(ordemDeCompraExistente.Id, cancellationToken);

                ordemDeCompraExistente.CodigoFornecedor = ordemDeCompraDto.CodigoFornecedor;
                ordemDeCompraExistente.CodigoErpFornecedor = ordemDeCompraDto.CodigoErpFornecedor;
                ordemDeCompraExistente.PaisImportador = ordemDeCompraDto.PaisImportador;
                ordemDeCompraExistente.NumeroOrdem = ordemDeCompraDto.NumeroOrdem;
                ordemDeCompraExistente.DataOrdem = ordemDeCompraDto.DataOrdem?.ToUniversalTime();
                ordemDeCompraExistente.NumeroRevisao = ordemDeCompraDto.NumeroRevisao?.ToString();
                ordemDeCompraExistente.DataRevisao = ordemDeCompraDto.DataRevisao?.ToUniversalTime();
                ordemDeCompraExistente.CodigoEstabelecimento = ordemDeCompraDto.CodigoEstabelecimento;
                ordemDeCompraExistente.DataExp = ordemDeCompraDto.DataExp?.ToUniversalTime();
                ordemDeCompraExistente.NomeFornecedor = ordemDeCompraDto.NomeFornecedor;
                ordemDeCompraExistente.EnderecoFornecedor = ordemDeCompraDto.EnderecoFornecedor;
                ordemDeCompraExistente.NumeroEnderecoFornecedor = ordemDeCompraDto.NumeroEnderecoFornecedor;
                ordemDeCompraExistente.ContatoFornecedor = ordemDeCompraDto.ContatoFornecedor;
                ordemDeCompraExistente.EmailFornecedor = ordemDeCompraExistente.Fornecedor.Email;
                ordemDeCompraExistente.CodigoFabricante = ordemDeCompraDto.CodigoFabricante;
                ordemDeCompraExistente.NomeFabricante = ordemDeCompraDto.NomeFabricante;
                ordemDeCompraExistente.EnderecoFabricante = ordemDeCompraDto.EnderecoFabricante;
                ordemDeCompraExistente.NomeImportador = ordemDeCompraDto.NomeImportador;
                ordemDeCompraExistente.EnderecoImportador = ordemDeCompraDto.EnderecoImportador;
                ordemDeCompraExistente.ContatoImportador = ordemDeCompraDto.ContatoImportador;
                ordemDeCompraExistente.EmailImportador = ordemDeCompraDto.EmailImportador;
                ordemDeCompraExistente.NumeroEnderecoImportador = ordemDeCompraDto.NumeroEnderecoImportador;
                ordemDeCompraExistente.ComplementoEnderecoImportador = ordemDeCompraDto.ComplementoEnderecoImportador;
                ordemDeCompraExistente.BairroEnderecoImportador = ordemDeCompraDto.BairroEnderecoImportador;
                ordemDeCompraExistente.EstadoProvinciaImportador = ordemDeCompraDto.EstadoProvinciaImportador;
                ordemDeCompraExistente.ZipCodeImportador = ordemDeCompraDto.ZipCodeImportador;
                ordemDeCompraExistente.CnpjImportador = ordemDeCompraDto.CnpjImportador;
                ordemDeCompraExistente.PrazoPagamento = ordemDeCompraDto.PrazoPagamento;
                ordemDeCompraExistente.TipoFrete = ordemDeCompraDto.TipoFrete;
                ordemDeCompraExistente.ModoEntrega = ordemDeCompraDto.ModoEntrega;
                ordemDeCompraExistente.NomeAgente = ordemDeCompraDto.NomeAgente;
                ordemDeCompraExistente.Destino = ordemDeCompraDto.Destino;
                ordemDeCompraExistente.NumeroContainer20 = ordemDeCompraDto.NumeroContainer20;
                ordemDeCompraExistente.NumeroContainer40 = ordemDeCompraDto.NumeroContainer40;
                ordemDeCompraExistente.NumeroContainer40HC = ordemDeCompraDto.NumeroContainer40HC;
                ordemDeCompraExistente.NumeroContainerOutros = ordemDeCompraDto.NumeroContainerOutros;
                ordemDeCompraExistente.TotalCBM = ordemDeCompraDto.TotalCBM;
                ordemDeCompraExistente.PesoTotal = ordemDeCompraDto.PesoTotal;
                ordemDeCompraExistente.NomeComprador = ordemDeCompraDto.NomeComprador;
                ordemDeCompraExistente.NomeVendedor = ordemDeCompraDto.NomeVendedor;
                ordemDeCompraExistente.DataAlteracao = ordemDeCompraDto.DataAlteracao?.ToUniversalTime();
                ordemDeCompraExistente.TaxaEmbalagem = ordemDeCompraDto.TaxaEmbalagem;
                ordemDeCompraExistente.TaxaInterna = ordemDeCompraDto.TaxaInterna;
                ordemDeCompraExistente.OutrasDespesas = ordemDeCompraDto.OutrasDespesas;
                ordemDeCompraExistente.Desconto = ordemDeCompraDto.Desconto;
                ordemDeCompraExistente.Frete = ordemDeCompraDto.Frete;
                ordemDeCompraExistente.AcordoFornecimento = RetornarAcordoFornecimentoFormatado(ordemDeCompraDto.AcordoFornecimento);
                ordemDeCompraExistente.Produtos = ordemDeCompraDto.Produtos.Select(produtoDto =>
                    new OrdemDeCompraProduto
                    {
                        OrdemItem = produtoDto.OrdemItem,
                        CodigoItem = produtoDto.CodigoItem,
                        CodigoSKU = produtoDto.Sku,
                        Descricao = produtoDto.Descricao,
                        UnidadeMedida = produtoDto.UnidadeMedida,
                        CodigoNCM = produtoDto.Ncm,
                        Quantidade = produtoDto.Quantidade,
                        MoedaSigla = produtoDto.Moeda,
                        ValorUnitario = produtoDto.ValorUnitario,
                        ValorTotal = produtoDto.ValorTotal,
                        DataEstimadaPartida = produtoDto.Etd?.AddDays(-7).ToUniversalTime(),
                        Etd = produtoDto.Etd?.ToUniversalTime(),
                        NumeroLote = produtoDto.NumeroLote,
                        PosicaoItem = produtoDto.PosicaoItem
                    }).ToList();

                ordemDeCompraExistente.ValorTotal = (ordemDeCompraExistente.Produtos.Sum(produto => produto.ValorTotal) + ordemDeCompraExistente.OutrasDespesas + ordemDeCompraExistente.Frete + ordemDeCompraExistente.TaxaEmbalagem + ordemDeCompraExistente.TaxaInterna) - ordemDeCompraExistente.Desconto;
                ordemDeCompraExistente.UnidadesMedida = CalcularTotalPorUnidade(ordemDeCompraExistente.Produtos, ordemDeCompraExistente);

                if (ordemDeCompraExistente.NumeroRevisao != ordemDeCompraDto.NumeroRevisao?.ToString())
                    ordemDeCompraExistente.Status = EStatusOrdemDeCompra.AguardandoPi;

                repositoryOrdemDeCompra.Update(ordemDeCompraExistente);
            }
        };

        empresa.DataUltimaAtualizacaoOrdemDeCompra = DateTime.UtcNow;
        repositoryEmpresa.Update(empresa);

        await mediator.Publish(new AuditoriaAdicionarInput(Guid.NewGuid(), TabelasResources.OrdemCompra, "Criado", ETipoAuditoria.Adicionado), cancellationToken);
        return new CommandResponse<OrdemDeCompraAdicionarResponse>(new OrdemDeCompraAdicionarResponse(OrdemDeCompraResources.OrdemDeCompraAdicionadaComSucesso), this);
    }

    private List<OrdemDeCompraUnidadeMedida> CalcularTotalPorUnidade(IEnumerable<OrdemDeCompraProduto> produtos, Entities.OrdemDeCompra ordemDeCompra)
    {
        return produtos
            .GroupBy(produto => produto.UnidadeMedida)
            .Select(grupo => new OrdemDeCompraUnidadeMedida(ordemDeCompra, grupo.Key, grupo.Sum(produto => produto.Quantidade)))
            .ToList();
    }

    private string RetornarAcordoFornecimentoFormatado(string? acordoFornecimento)
    {
        if (string.IsNullOrEmpty(acordoFornecimento))
            return string.Empty;

        AcordoFornecimentoFormatado.Clear();
        var acordoFornecimentoPorLinha = acordoFornecimento; //.Replace("\t", "").Split("\n");
        foreach (var linha in acordoFornecimentoPorLinha)
            AcordoFornecimentoFormatado.Append(linha);

        return AcordoFornecimentoFormatado.ToString();
    }
}