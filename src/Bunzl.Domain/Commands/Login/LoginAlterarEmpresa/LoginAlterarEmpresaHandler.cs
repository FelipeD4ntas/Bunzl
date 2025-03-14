using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using Bunzl.Infra.CrossCutting.Security.Token;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Bunzl.Domain.Commands.Login.LoginAlterarEmpresa;

public class LoginAlterarEmpresaHandler(IConfiguration configuration, IRepositoryUsuario repositoryUsuario, IUsuarioAutenticado usuarioAutenticado)
    : Notifiable, IRequestHandler<LoginAlterarEmpresaRequest, CommandResponse<LoginAlterarEmpresaResponse>>
{
    public async Task<CommandResponse<LoginAlterarEmpresaResponse>> Handle(LoginAlterarEmpresaRequest request, CancellationToken cancellationToken)
    {
        var usuario = await repositoryUsuario.GetByAsync(true, p => p.Id == usuarioAutenticado.UsuarioId, cancellationToken, p => p.Empresas);
        if (usuario is null)
        {
            AddNotification("Usuário", LoginResources.LoginNaoIdentificado);
            return await Task.FromResult(new CommandResponse<LoginAlterarEmpresaResponse>(this));
        }

        if (!usuario.FlagAtivo)
        {
            AddNotification("Usuário", LoginResources.LoginUsuarioInvativo);
            return await Task.FromResult(new CommandResponse<LoginAlterarEmpresaResponse>(this));
        }

        if (usuario.Senha == null)
        {
            AddNotification("Usuario", LoginResources.LoginCadastroNaoFinalizado);
            return await Task.FromResult(new CommandResponse<LoginAlterarEmpresaResponse>(this));
        }

        if (usuario.Empresas == null || !usuario.Empresas.Any())
        {
            AddNotification("Usuário", LoginResources.LoginUsuarioSemEmpresa);
            return await Task.FromResult(new CommandResponse<LoginAlterarEmpresaResponse>(this));
        }

        if (!usuario.Empresas.Any(x => x.Id == request.EmpresaId))
        {
            AddNotification("Usuário", LoginResources.LoginEmpresaInvalidaProUsuario);
            return await Task.FromResult(new CommandResponse<LoginAlterarEmpresaResponse>(this));
        }

        var empresa = usuario.Empresas.FirstOrDefault(e => e.Id == request.EmpresaId);

        if (empresa == null)
        {
            AddNotification("Empresa", EmpresaResources.EmpresaNaoEncontrada);
            return await Task.FromResult(new CommandResponse<LoginAlterarEmpresaResponse>(this));
        }

        usuario.UltimoLogin = DateTime.UtcNow;

        var token = new TokenBuilder(configuration)
            .WithUserId(usuario.Id.ToString())
            .WithUserName(usuario.Nome)
            .WithUserCompany(request.EmpresaId!.Value.ToString())
            .WithProfile(usuario.PerfilPermissao.ToString())
            .WithIdioma(request.Idioma)
            .WithCnpjEmpresa(empresa.Cnpj)
			.Build(usuarioAutenticado.Expiracao);

        return new CommandResponse<LoginAlterarEmpresaResponse>(new LoginAlterarEmpresaResponse(usuario.Nome, usuario.Email, token), this);
    }
}