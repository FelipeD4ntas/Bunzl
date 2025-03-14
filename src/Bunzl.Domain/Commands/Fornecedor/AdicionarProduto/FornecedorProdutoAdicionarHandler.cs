using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.AdicionarProduto;

public class FornecedorProdutoAdicionarHandler(
    IPublisher mediator,
    IRepositoryFornecedor repositoryFornecedor,
    IRepositoryUsuario repositoryUsuario,
    IUsuarioAutenticado usuarioAutenticado)
    : Notifiable, IRequestHandler<FornecedorProdutoAdicionarRequest, CommandResponse<FornecedorProdutoAdicionarResponse>>
{
    public async Task<CommandResponse<FornecedorProdutoAdicionarResponse>> Handle(FornecedorProdutoAdicionarRequest request, CancellationToken cancellationToken)
    {
        var fornecedor = await repositoryFornecedor.GetByAsync(true, f => f.Id == request.FornecedorId, cancellationToken, p => p.FornecedorProdutos);
        if (fornecedor == null)
        {
            AddNotification("Fornecedor", FornecedorResources.FornecedorNaoEncontrado);
            return new CommandResponse<FornecedorProdutoAdicionarResponse>(this);
        }

        var usuarios = await repositoryUsuario.ListAsync(true, u => u.PerfilPermissao == EPerfilUsuario.CompradorKeyUser || u.PerfilPermissao == EPerfilUsuario.AdministradorSuperUser, u => u.Empresas);
        var usuariosAutenticados = usuarios
             .Where(u => u.Empresas.Any(e => e.Id == usuarioAutenticado.UsuarioEmpresa))
             .ToList();

        var fornecedorProduto = new Entities.FornecedorProduto
        (
            fornecedor.Id,
            request.CodigoFornecedor,
            request.DescricaoCompletaFornecedor,
            request.AplicacoesPrincipais,
            request.Composicao,
            request.Tamanho,
            request.Cor,
            request.CodigoNCM,
            request.UnidadeMedidaFornecedorMOQ,
            request.UnidadeMedidaFornecedorPreco,
            request.QuantidadeMinimaPedido,
            request.Preco,
            request.IncotermId,
            request.TermoPagamento,
            request.Observacoes,
            request.DetalhesEmbalagem,
            request.PesoBruto,
            request.Comprimento,
            request.Largura,
            request.Altura,
            request.TempoEntrega,
            request.CustoDesenvolvimentoEmbalagem,
            request.CustoRotulagemEmbalagem,
            request.PortoEmbarque,
            request.QuantidadeCarregamentoContainer20Ft,
            request.QuantidadeCarregamentoContainer40Ft,
            request.QuantidadeCarregamentoContainer40Hc,
			request.TipoEmbalagemInterna,
			request.QuantidadePorEmbalagemInterna,
			request.TipoCaixaMaster,
			request.QuantidadePorCaixaMaster,
			request.CapacidadeMensalFabrica,
			request.UnidadeMedidaCapacidadeMensal,
			request.NomeFabrica,
			request.CustoDetalhadoMateriaPrima,
			request.CustoDetalhadoCombustivel,
			request.CustoDetalhadoEmbalagem,
			request.CustoDetalhadoMaoDeObra,
			request.CustoDetalhadoEnergia,
			request.CustoDetalhadoTransporte
		);

        fornecedor.AdicionarProduto(fornecedorProduto);

        if (IsInvalid())
            return await Task.FromResult(new CommandResponse<FornecedorProdutoAdicionarResponse>(this));

        repositoryFornecedor.Update(fornecedor);
        await mediator.Publish(new AuditoriaAdicionarInput(fornecedorProduto.Id, TabelasResources.FornecedorProduto, "Produto", ETipoAuditoria.Modificado));

        return new CommandResponse<FornecedorProdutoAdicionarResponse>(
            new FornecedorProdutoAdicionarResponse(
                fornecedorProduto.Id, 
                FornecedorResources.ProdutoAdicionadoComSucesso, 
                fornecedorProduto, 
                fornecedor.NomeFantasia, 
                usuariosAutenticados,
                fornecedorProduto.DescricaoCompletaFornecedor),
            this);
    }
}
