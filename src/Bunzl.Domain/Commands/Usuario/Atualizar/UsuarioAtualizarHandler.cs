using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using MediatR;

namespace Bunzl.Domain.Commands.Usuario.Atualizar;

public class UsuarioAtualizarHandler(IPublisher mediator, IRepositoryUsuario repositoryUsuario, IUsuarioAutenticado usuarioAutenticado) : Notifiable,
    IRequestHandler<UsuarioAtualizarRequest, CommandResponse<UsuarioAtualizarResponse>>
{
    public async Task<CommandResponse<UsuarioAtualizarResponse>> Handle(UsuarioAtualizarRequest request,
        CancellationToken cancellationToken)
    {
        var emailExists = await repositoryUsuario.ExistsAsync(p => p.Email == request.Email && p.Id != request.Id, false, cancellationToken);
        if (emailExists)
            AddNotification("Email", UsuarioResources.EmailDuplicado);

        if (string.IsNullOrEmpty(request.Nome) && string.IsNullOrEmpty(request.Email))
            AddNotification("Usuario", UsuarioResources.UsuarioSemAlteracoes);

        if (IsInvalid())
            return new CommandResponse<UsuarioAtualizarResponse>(this);

        var usuario = await repositoryUsuario.GetByAsync(true, c => c.Id == request.Id, cancellationToken, p => p.Empresas);
        if (usuario == null)
        {
            AddNotification("Usuario", UsuarioResources.UsuarioNaoEncontrado);
            return new CommandResponse<UsuarioAtualizarResponse>(this);
        }

        if (request.Nome == usuario.Nome && request.Email == usuario.Email)
            AddNotification("Usuario", UsuarioResources.UsuarioSemAlteracoes);

        var perfilSolicitantePodeEditar = PerfilSolicitantePodeEditar(usuario);
        if (!perfilSolicitantePodeEditar)
            AddNotification("EPerfilUsuario", UsuarioResources.PerfilInvalido);

        if (IsInvalid())
            return new CommandResponse<UsuarioAtualizarResponse>(this);

        if (!string.IsNullOrEmpty(request.Email) &&
            request.Email != usuario.Email)
        {
            usuario.GerarChaveCadastro();
            if (usuario.ChaveCadastro == null)
            {
                AddNotification("ChaveCadastro", UsuarioResources.FalhaGerarChaveCadastro);
                return new CommandResponse<UsuarioAtualizarResponse>(this);
            }

            usuario.Senha = null;
            usuario.Telefone = null;
            usuario.Area = null;
        }

        usuario.Atualizar(
            !string.IsNullOrEmpty(request.Nome) ? request.Nome : usuario.Nome,
            !string.IsNullOrEmpty(request.Email) ? request.Email : usuario.Email,
            usuario.Telefone
            );

        AddNotifications(usuario.Notifications);

        if (IsInvalid())
            return new CommandResponse<UsuarioAtualizarResponse>(this);

        repositoryUsuario.Update(usuario);
        await mediator.Publish(new AuditoriaAdicionarInput(usuario.Id, TabelasResources.Usuario, "Atualizado nome e e-mail", ETipoAuditoria.Modificado));

        return new CommandResponse<UsuarioAtualizarResponse>(
            new UsuarioAtualizarResponse(
                usuario.Id,
                UsuarioResources.UsuarioAtualizadoComSucesso,
                usuario.Nome,
                usuario.Email,
                usuario.ChaveCadastro != null ? usuario.ChaveCadastro.Value : null),
            this);
    }

    private bool PerfilSolicitantePodeEditar(Entities.Usuario usuario)
    {
        var perfilSolicitante = usuarioAutenticado.Permissoes;
        var empresaSolicitante = usuarioAutenticado.UsuarioEmpresa;

        if (perfilSolicitante == Enumerators.EPerfilUsuario.BunzlCorporativoMasterUser.ToString())
            return true;

        if (empresaSolicitante != usuario.Empresas.FirstOrDefault()!.Id)
            return false;

        if (perfilSolicitante == Enumerators.EPerfilUsuario.AdministradorSuperUser.ToString() &&
            (usuario.PerfilPermissao == Enumerators.EPerfilUsuario.CompradorKeyUser ||
            usuario.PerfilPermissao == Enumerators.EPerfilUsuario.FornecedorEndUser))
            return true;

        if (perfilSolicitante == Enumerators.EPerfilUsuario.CompradorKeyUser.ToString() &&
            usuario.PerfilPermissao == Enumerators.EPerfilUsuario.FornecedorEndUser)
            return true;

        return false;
    }
}