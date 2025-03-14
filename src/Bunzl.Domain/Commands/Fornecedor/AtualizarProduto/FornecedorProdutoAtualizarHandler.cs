using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.AtualizarProduto;

public class FornecedorProdutoAtualizarHandler(
    IPublisher mediator,
    IRepositoryFornecedor repositoryFornecedor,
    IRepositoryUsuario repositoryUsuario,
    IUsuarioAutenticado usuarioAutenticado)
    : Notifiable, IRequestHandler<FornecedorProdutoAtualizarRequest, CommandResponse<FornecedorProdutoAtualizarResponse>>
{
    public async Task<CommandResponse<FornecedorProdutoAtualizarResponse>> Handle(FornecedorProdutoAtualizarRequest request, CancellationToken cancellationToken)
    {

        var fornecedor = await repositoryFornecedor.GetByAsync(true, c => c.Id == request.FornecedorId, cancellationToken, p => p.FornecedorProdutos);
        if (fornecedor == null)
        {
            AddNotification("Fornecedor", FornecedorResources.FornecedorNaoEncontrado);
            return new CommandResponse<FornecedorProdutoAtualizarResponse>(this);
        }

        var produto = fornecedor.FornecedorProdutos.FirstOrDefault(x => x.Id == request.Id);
        if (produto == null)
        {
            AddNotification("Produto", FornecedorResources.ProdutoNaoEncontrado);
            return new CommandResponse<FornecedorProdutoAtualizarResponse>(this);
        }

		var usuarios = await repositoryUsuario.ListAsync(
            true,
	        u => u.PerfilPermissao == EPerfilUsuario.CompradorKeyUser || 
            u.PerfilPermissao == EPerfilUsuario.AdministradorSuperUser);

		produto.Atualizar(request);

        repositoryFornecedor.Update(fornecedor);
        await mediator.Publish(new AuditoriaAdicionarInput(produto.Id, TabelasResources.FornecedorProduto, "Atualizado", ETipoAuditoria.Modificado), cancellationToken);

        var response = new FornecedorProdutoAtualizarResponse(
            produto.Id,
            FornecedorResources.ProdutoAtualizadoComSucesso,
            usuarioAutenticado.Permissoes.Contains(EPerfilUsuario.FornecedorEndUser.ToString()) ? usuarios.ToList() : null,
            usuarioAutenticado.Permissoes.Contains(EPerfilUsuario.FornecedorEndUser.ToString()) ? fornecedor.NomeFantasia : null,
            produto.CodigoSku,
            usuarioAutenticado.Permissoes.Contains(EPerfilUsuario.FornecedorEndUser.ToString()) ? produto.DescricaoCompletaFornecedor : null
        );

        return new CommandResponse<FornecedorProdutoAtualizarResponse>(response, this);
    }
}
