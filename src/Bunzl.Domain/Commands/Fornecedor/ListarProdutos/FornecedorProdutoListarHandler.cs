using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Domain.DTOs;
using Bunzl.Domain.Entities;
using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using MediatR;
using prmToolkit.EnumExtension;
using System.Linq.Expressions;

namespace Bunzl.Domain.Commands.Fornecedor.ListarProdutos;

public class FornecedorProdutoListarHandler(IRepositoryFornecedor repositoryFornecedor, IUsuarioAutenticado usuarioAutenticado)
    : Notifiable, IRequestHandler<FornecedorProdutoListarRequest, CommandResponse<DataSourcePageResponse>>
{
    public async Task<CommandResponse<DataSourcePageResponse>> Handle(FornecedorProdutoListarRequest request, CancellationToken cancellationToken)
    {
        var empresaUsuarioAtual = usuarioAutenticado.UsuarioEmpresa;
        var perfilUsuarioAtual = usuarioAutenticado.Permissoes.ToEnum<EPerfilUsuario>();

        var filtro = ObterFiltroPorPerfil(request.FornecedorId, empresaUsuarioAtual, perfilUsuarioAtual);

        var produtosResponse = await repositoryFornecedor
            .ListDevExpressFornecedorProdutosAsync<FornecedorProdutoDto>(
                false,
                request,
                filtro);

        //var responseFiltrado = FiltrarPropriedades(produtosResponse);

        return new CommandResponse<DataSourcePageResponse>(produtosResponse, this);
    }
    private DataSourcePageResponse FiltrarPropriedades(DataSourcePageResponse produtosResponse)
    {
        if (usuarioAutenticado.Permissoes.Contains(EPerfilUsuario.FornecedorEndUser.ToString()))
        {
            produtosResponse.Data = ((IEnumerable<FornecedorProdutoDto>)produtosResponse.Data).Select(p => new FornecedorProdutoDto
            {
                Id = p.Id,
                CodigoFornecedor = p.CodigoFornecedor,
                DescricaoCompletaFornecedor = p.DescricaoCompletaFornecedor,
                Preco = p.Preco,
                Status = p.Status
            }).ToList();
        }

        return produtosResponse;
    }

    private static Expression<Func<FornecedorProduto, bool>> ObterFiltroPorPerfil(Guid fornecedorId, Guid empresaUsuarioAtual, EPerfilUsuario perfilUsuarioLogado)
    {
        if (perfilUsuarioLogado == EPerfilUsuario.BunzlCorporativoMasterUser)
        {
            return fp => fp.Fornecedor.Id == fornecedorId;
        } 
        else
        {
            return fp =>
                fp.Fornecedor.Id == fornecedorId &&
                fp.Fornecedor.Empresas.Any(e => e.Id == empresaUsuarioAtual);
        }
    }

}
