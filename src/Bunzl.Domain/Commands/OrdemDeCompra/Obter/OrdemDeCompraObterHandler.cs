using Bunzl.Domain.DTOs;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.OrdemDeCompra.Obter;

public class OrdemDeCompraObterHandler(
    IRepositoryOrdemDeCompra repositoryOrdemDeCompra)
    : Notifiable, IRequestHandler<OrdemDeCompraObterRequest, CommandResponse<OrdemDeCompraObterResponse>>
{
    public async Task<CommandResponse<OrdemDeCompraObterResponse>> Handle(OrdemDeCompraObterRequest request, CancellationToken cancellationToken)
    {
        var ordemDeCompra = await repositoryOrdemDeCompra.GetByAsync(
            false,
            oc => oc.Id == request.Id,
            cancellationToken,
            oc => oc.Produtos,
            oc => oc.Observacoes,
            oc => oc.Anexos,
            oc => oc.UnidadesMedida);

        if (ordemDeCompra == null)
        {
            AddNotification("OrdemDeCompra", OrdemDeCompraResources.OrdemDeCompraNaoEncontrada);
            return new CommandResponse<OrdemDeCompraObterResponse>(this);
        }

        var produtos = ordemDeCompra.Produtos
            .Select(p => new OrdemDeCompraProdutoDto
            {
                Id = p.Id,
                OrdemDeCompraId = p.OrdemDeCompraId,
                OrdemItem = p.OrdemItem,
                CodigoItem = p.CodigoItem,
                CodigoSKU = p.CodigoSKU,
                Descricao = p.Descricao,
                UnidadeMedida = p.UnidadeMedida,
                CodigoNCM = p.CodigoNCM,
                Quantidade = p.Quantidade,
                MoedaSigla = p.MoedaSigla,
                ValorUnitario = p.ValorUnitario,
                ValorTotal = p.ValorTotal,
                DataEstimadaPartida = p.DataEstimadaPartida,
                Etd = p.Etd,
                NumeroLote = p.NumeroLote,
                PosicaoItem = p.PosicaoItem
            }).OrderBy(p => p.PosicaoItem).ToList();

        var observacoes = ordemDeCompra.Observacoes
            .Select(p => new OrdemDeCompraObservacaoDto
            {
                Id = p.Id,
                OrdemDeCompraId = p.OrdemDeCompraId,
                Observacao = p.Observacao
            }).ToList();

        var anexos = ordemDeCompra.Anexos
            .Select(p => new OrdemDeCompraAnexoDto
            {
                Id = p.Id,
                OrdemDeCompraId = p.OrdemDeCompraId,
                Nome = p.Nome,
                Tipo = p.Tipo,
                DataCriacao = p.DataCriacao
            }).ToList();

        var unidadesMedida = ordemDeCompra.UnidadesMedida
            .Select(p => new OrdemDeCompraUnidadeMedidaDto
            {
                Id = p.Id,
                OrdemDeCompraId = p.OrdemDeCompraId,
                UnidadeMedida = p.UnidadeMedida,
                QuantidadeTotal = p.QuantidadeTotal
            }).ToList();

        var response = new OrdemDeCompraObterResponse(
            ordemDeCompra.Id,
            ordemDeCompra.FornecedorId,
            ordemDeCompra.EmpresaId,
            ordemDeCompra.CodigoFornecedor,
            ordemDeCompra.CodigoErpFornecedor,
            ordemDeCompra.PaisImportador,
            ordemDeCompra.NumeroOrdem,
            ordemDeCompra.DataOrdem,
            ordemDeCompra.NumeroRevisao,
            ordemDeCompra.DataRevisao,
            ordemDeCompra.CodigoEstabelecimento,
            ordemDeCompra.DataExp,
            ordemDeCompra.NomeFornecedor,
            ordemDeCompra.EnderecoFornecedor,
            ordemDeCompra.NumeroEnderecoFornecedor,
            ordemDeCompra.ContatoFornecedor,
            ordemDeCompra.EmailFornecedor,
            ordemDeCompra.CodigoFabricante,
            ordemDeCompra.NomeFabricante,
            ordemDeCompra.EnderecoFabricante,
            ordemDeCompra.NomeImportador,
            ordemDeCompra.EnderecoImportador,
            ordemDeCompra.ContatoImportador,
            ordemDeCompra.EmailImportador,
            ordemDeCompra.NumeroEnderecoImportador,
            ordemDeCompra.ComplementoEnderecoImportador,
            ordemDeCompra.BairroEnderecoImportador,
            ordemDeCompra.EstadoProvinciaImportador,
            ordemDeCompra.ZipCodeImportador,
            ordemDeCompra.CnpjImportador,
            ordemDeCompra.PrazoPagamento,
            ordemDeCompra.TipoFrete,
            ordemDeCompra.ModoEntrega,
            ordemDeCompra.NomeAgente,
            ordemDeCompra.Destino,
            ordemDeCompra.NumeroContainer20,
            ordemDeCompra.NumeroContainer40,
            ordemDeCompra.NumeroContainer40HC,
            ordemDeCompra.NumeroContainerOutros,
            ordemDeCompra.TotalCBM,
            ordemDeCompra.PesoTotal,
            ordemDeCompra.NomeComprador,
            ordemDeCompra.NomeVendedor,
            ordemDeCompra.DataAlteracao,
            ordemDeCompra.TaxaEmbalagem,
            ordemDeCompra.TaxaInterna,
            ordemDeCompra.OutrasDespesas,
            ordemDeCompra.Desconto,
            ordemDeCompra.Frete,
            ordemDeCompra.AcordoFornecimento,
            ordemDeCompra.ValorTotal,
            ordemDeCompra.Status,
            produtos,
            anexos,
            observacoes,
            unidadesMedida);

        return new CommandResponse<OrdemDeCompraObterResponse>(response, this);
    }
}

