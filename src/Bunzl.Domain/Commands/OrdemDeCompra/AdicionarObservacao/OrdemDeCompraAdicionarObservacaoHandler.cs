using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;
using System.Globalization;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;

namespace Bunzl.Domain.Commands.OrdemDeCompra.AdicionarObservacao;

public class OrdemDeCompraHandler(
    IPublisher mediator,
    IRepositoryOrdemDeCompra repositoryOrdemDeCompra,
    IRepositoryEmpresa repositoryEmpresa,
    IRepositoryFornecedor repositoryFornecedor,
    IRepositoryUsuario repositoryUsuario,
    IUsuarioAutenticado usuarioAutenticado)
    : Notifiable, IRequestHandler<OrdemDeCompraAdicionarObservacaoRequest, CommandResponse<OrdemDeCompraAdicionarObservacaoResponse>>
{
    public async Task<CommandResponse<OrdemDeCompraAdicionarObservacaoResponse>> Handle(OrdemDeCompraAdicionarObservacaoRequest request,
        CancellationToken cancellationToken)
    {
        var ordemDeCompra = await repositoryOrdemDeCompra.GetByAsync(true,
            f => f.Id == request.OrdemDeCompraId, cancellationToken, p => p.Observacoes);
        if (ordemDeCompra == null)
        {
            AddNotification("OrdemDeCompra", OrdemDeCompraResources.OrdemDeCompraNaoEncontrada);
            return new CommandResponse<OrdemDeCompraAdicionarObservacaoResponse>(this);
        }

        var ordemDeCompraObservacao = new Entities.OrdemDeCompraObservacao(request.Observacao, ordemDeCompra.Id);
        ordemDeCompra.AdicionarObservacao(ordemDeCompraObservacao);

        if (IsInvalid())
            return await Task.FromResult(new CommandResponse<OrdemDeCompraAdicionarObservacaoResponse>(this));

        repositoryOrdemDeCompra.Update(ordemDeCompra);

        var fornecedor = await repositoryFornecedor.GetByAsync(true, f => f.Id == ordemDeCompra.FornecedorId, cancellationToken, p => p.Usuarios);
        if (fornecedor == null)
        {
            AddNotification("Fornecedor", FornecedorResources.FornecedorNaoEncontrado);
            return new CommandResponse<OrdemDeCompraAdicionarObservacaoResponse>(this);
        }

        var usuario = await repositoryUsuario.GetByAsync(true, u => u.Id == usuarioAutenticado.UsuarioId, cancellationToken, u => u.Empresas);
        if (usuario == null)
        {
            AddNotification("Fornecedor", UsuarioResources.UsuarioNaoEncontrado);
            return new CommandResponse<OrdemDeCompraAdicionarObservacaoResponse>(this);
        }

        var empresa = await repositoryEmpresa.GetByAsync(true, u => u.Id == ordemDeCompra.EmpresaId, false, cancellationToken, e => e.Usuarios);
        if (empresa == null)
        {
	        AddNotification("Empresa", EmpresaResources.EmpresaNaoEncontrada);
	        return new CommandResponse<OrdemDeCompraAdicionarObservacaoResponse>(this);
        }

		await mediator.Publish(new AuditoriaAdicionarInput(ordemDeCompra.Id, TabelasResources.OrdemCompra,
            "Observação Adicionada", ETipoAuditoria.Modificado));

        if (usuarioAutenticado.Permissoes == EPerfilUsuario.FornecedorEndUser.ToString())
        {
            var usuariosEmpresaEmail = empresa.Usuarios.Where(u => u.PerfilPermissao != EPerfilUsuario.FornecedorEndUser).ToList();

            return new CommandResponse<OrdemDeCompraAdicionarObservacaoResponse>(
                new OrdemDeCompraAdicionarObservacaoResponse(ordemDeCompra.Id, OrdemDeCompraResources.ObservacaoAdicionadaComSucesso, fornecedor, ordemDeCompra.NumeroOrdem, usuario.Nome, empresa, usuariosEmpresaEmail),
                this);
        }
        else
        {
            return new CommandResponse<OrdemDeCompraAdicionarObservacaoResponse>(
                new OrdemDeCompraAdicionarObservacaoResponse(ordemDeCompra.Id, OrdemDeCompraResources.ObservacaoAdicionadaComSucesso, fornecedor, ordemDeCompra.NumeroOrdem, usuario.Nome, empresa),
                this);
        }
    }
}
