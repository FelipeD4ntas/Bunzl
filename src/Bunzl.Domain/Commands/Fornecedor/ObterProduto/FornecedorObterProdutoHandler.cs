using Bunzl.Domain.DTOs;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.ObterProduto;

public class FornecedorObterProdutoHandler(IRepositoryFornecedor repositoryFornecedor)
    : Notifiable, IRequestHandler<FornecedorObterProdutoRequest, CommandResponse<FornecedorObterProdutoResponse>>
{
    public async Task<CommandResponse<FornecedorObterProdutoResponse>> Handle(FornecedorObterProdutoRequest request, CancellationToken cancellationToken)
    {
        var fornecedor = await repositoryFornecedor.GetByAsync(
           true,
           f => f.Id == request.FornecedorId,
           cancellationToken,
           "Moeda", "FornecedorProdutos", "FornecedorProdutos.FornecedorProdutoAnexos"
       );

        if (fornecedor == null)
        {
            AddNotification("Fornecedor", FornecedorResources.FornecedorNaoEncontrado);
            return new CommandResponse<FornecedorObterProdutoResponse>(this);
        }

        var produto = fornecedor.FornecedorProdutos
            .FirstOrDefault(p => p.Id == request.ProdutoId);

        if (produto == null)
        {
            AddNotification("Produto", FornecedorResources.ProdutoNaoEncontrado);
            return new CommandResponse<FornecedorObterProdutoResponse>(this);
        }

        var fornecedorProdutoAnexos = produto.FornecedorProdutoAnexos
           .Select(anexo => new FornecedorProdutoAnexoDto
           {
               Id = anexo.Id,
               Nome = anexo.Nome,
               Tipo = anexo.Tipo,
               TipoDocumento = anexo.TipoDocumento,
               Observacao = anexo.Observacao,
               DataCriacao = anexo.DataCriacao,
               FornecedorProdutoId = anexo.FornecedorProdutoId
           }).ToList();

        var response = new FornecedorObterProdutoResponse(
            produto.Id,
            produto.FornecedorId,
            new MoedaDto
            {
                Id = fornecedor.MoedaId,
                Descricao = fornecedor.Moeda.Descricao,
                Sigla = fornecedor.Moeda.Sigla
            },
            produto.CodigoFornecedor,
            produto.DescricaoCompletaFornecedor,
            produto.DescricaoCompletaBunzl,
            produto.AplicacoesPrincipais,
            produto.Composicao,
            produto.Tamanho,
            produto.Cor,
            produto.CodigoNCM,
            produto.UnidadeMedidaFornecedorMOQ,
            produto.UnidadeMedidaFornecedorPreco,
            produto.UnidadeMedidaBunzl,
            produto.QuantidadeMinimaPedido,
            produto.Preco,
            produto.IncotermId,
            produto.TermoPagamento,
            produto.Observacoes,
            produto.DetalhesEmbalagem,
            produto.PesoBruto,
            produto.Comprimento,
            produto.Largura,
            produto.Altura,
            produto.CBM,
            produto.TempoEntrega,
            produto.CustoDesenvolvimentoEmbalagem,
            produto.CustoRotulagemEmbalagem,
            produto.PortoEmbarque,
            produto.QuantidadeCarregamentoContainer20Ft,
            produto.QuantidadeCarregamentoContainer40Ft,
            produto.QuantidadeCarregamentoContainer40Hc,
            produto.CodigoArtigo,
            produto.Familia,
            produto.CodigoSku,
            produto.CorBunzl,
            produto.TamanhoBunzl,
            produto.Status,
            produto.TipoEmbalagemInterna,
            produto.QuantidadePorEmbalagemInterna,
            produto.TipoCaixaMaster,
            produto.QuantidadePorCaixaMaster,
            produto.CapacidadeMensalFabrica,
            produto.UnidadeMedidaCapacidadeMensal,
            produto.NomeFabrica,
            produto.CustoDetalhadoMateriaPrima,
            produto.CustoDetalhadoCombustivel,
            produto.CustoDetalhadoEmbalagem,
            produto.CustoDetalhadoMaoDeObra,
            produto.CustoDetalhadoEnergia,
            produto.CustoDetalhadoTransporte,
			fornecedorProdutoAnexos);

        return new CommandResponse<FornecedorObterProdutoResponse>(response, this);
    }
}
