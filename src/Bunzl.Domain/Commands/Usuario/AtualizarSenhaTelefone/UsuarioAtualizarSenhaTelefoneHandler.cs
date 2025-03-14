using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.Extensions;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.Usuario.AtualizarSenhaTelefone;

public class UsuarioAtualizarSenhaTelefoneHandler(IPublisher mediator, IRepositoryUsuario repositoryUsuario)
    : Notifiable, IRequestHandler<UsuarioAtualizarSenhaTelefoneRequest, CommandResponse<UsuarioAtualizarSenhaTelefoneResponse>>
{
    public async Task<CommandResponse<UsuarioAtualizarSenhaTelefoneResponse>> Handle(UsuarioAtualizarSenhaTelefoneRequest request, CancellationToken cancellationToken)
    {
        var usuario = await repositoryUsuario.GetByAsync(true, u => u.Id == request.Id, false, cancellationToken);
        if (usuario == null)
        {
            AddNotification("Usuario", UsuarioResources.UsuarioNaoEncontrado);
            return new CommandResponse<UsuarioAtualizarSenhaTelefoneResponse>(this);
        }

        if (usuario.Senha != request.SenhaAtual.EncryptPassword())
        {
            AddNotification("Usuario", UsuarioResources.UsuarioSenhaInvalido);
            return new CommandResponse<UsuarioAtualizarSenhaTelefoneResponse>(this);
        }

        var teveAlteracao = false;
        if (!string.IsNullOrEmpty(request.NovaSenha))
        {
            usuario.AlterarSenha(request.NovaSenha);
            teveAlteracao = true;
        }

        if (request.Telefone != usuario.Telefone || request.Area != usuario.Area)
        {
            usuario.Telefone = request.Telefone;
            usuario.Area = request.Area;
            teveAlteracao = true;
        }

        if (teveAlteracao)
        {
            repositoryUsuario.Update(usuario);

            await mediator.Publish(
                new AuditoriaAdicionarInput(
                    usuario.Id,
                    TabelasResources.Usuario,
                    "Atualizado senha e telefone",
                    ETipoAuditoria.Modificado));

            return new CommandResponse<UsuarioAtualizarSenhaTelefoneResponse>(
                new UsuarioAtualizarSenhaTelefoneResponse(usuario.Id, UsuarioResources.SenhaETelefoneAlteradosComSucesso),
                this);
        }

        return new CommandResponse<UsuarioAtualizarSenhaTelefoneResponse>(
            new UsuarioAtualizarSenhaTelefoneResponse(usuario.Id, UsuarioResources.SenhaETelefoneNaoAlterados),
            this);
    }
}
