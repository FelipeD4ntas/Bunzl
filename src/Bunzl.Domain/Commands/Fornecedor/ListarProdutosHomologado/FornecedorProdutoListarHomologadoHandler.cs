using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Domain.DTOs;
using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.ListarProdutosHomologado;

public class FornecedorProdutoListarHomologadoHandler(
    IRepositoryFornecedor repositoryFornecedor, 
    IRepositoryProduto repositoryProduto,
    IUsuarioAutenticado usuarioAutenticado)
    : Notifiable, IRequestHandler<FornecedorProdutoListarHomologadoRequest, CommandResponse<DataSourcePageResponse>>
{
    public async Task<CommandResponse<DataSourcePageResponse>> Handle(
        FornecedorProdutoListarHomologadoRequest request, CancellationToken cancellationToken)
    {
        var produtosResponse = await repositoryFornecedor
	        .ListDevExpressFornecedorProdutosAsync<FornecedorProdutoDto>(
		        false,
		        request,
		        p => p.FornecedorId == request.FornecedorId && p.Status == EStatusProduto.Homologado
				);

        var responseFiltrado = FiltrarPropriedades(produtosResponse);

        return new CommandResponse<DataSourcePageResponse>(responseFiltrado, this);
	}

    private DataSourcePageResponse FiltrarPropriedades(DataSourcePageResponse produtosResponse)
    {
	    if (usuarioAutenticado.Permissoes.Contains(EPerfilUsuario.FornecedorEndUser.ToString()))
	    {
		    produtosResponse.Data = ((IEnumerable<FornecedorProdutoDto>)produtosResponse.Data).Select(p => new FornecedorProdutoDto
		    {
			    Id = p.Id,
				CodigoSku = p.CodigoSku,
			    CodigoFornecedor = p.CodigoFornecedor,
			    DescricaoCompletaFornecedor = p.DescricaoCompletaFornecedor,
			    Preco = p.Preco,
			    Status = p.Status
		    }).ToList();
	    }

	    return produtosResponse;
    }
}
    
