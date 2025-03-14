using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Domain.DTOs;
using Bunzl.Domain.Entities;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using MediatR;
using System.Linq.Expressions;

namespace Bunzl.Domain.Commands.Fornecedor.ListarAnexosProduto;

public class FornecedorProdutoListarAnexoHandler(IRepositoryFornecedor repositoryFornecedor)
    : Notifiable, IRequestHandler<FornecedorProdutoListarAnexoRequest, CommandResponse<DataSourcePageResponse>>
{
    public async Task<CommandResponse<DataSourcePageResponse>> Handle(FornecedorProdutoListarAnexoRequest request, CancellationToken cancellationToken)
    {
        var filtro = ObterFiltroPorPerfil(request.FornecedorProdutoId);

        var produtosResponse = await repositoryFornecedor
            .ListDevExpressFornecedorProdutosAnexosAsync<FornecedorProdutoAnexoDto>(
                false,
                request,
                filtro);

        return new CommandResponse<DataSourcePageResponse>(produtosResponse, this);
    }

    private static Expression<Func<FornecedorProdutoAnexo, bool>> ObterFiltroPorPerfil(Guid fornecedorProdutoId)
    {
        return fp => fp.FornecedorProdutoId == fornecedorProdutoId;
    }
}
