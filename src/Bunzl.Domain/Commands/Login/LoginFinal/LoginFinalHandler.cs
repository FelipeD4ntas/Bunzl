using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.Extensions;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Infra.CrossCutting.Security.Token;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Bunzl.Domain.Commands.Login.LoginFinal;

public class LoginFinalHandler(IConfiguration configuration, IRepositoryUsuario repositoryUsuario, IRepositoryEmpresa repositoryEmpresa) : Notifiable, IRequestHandler<LoginFinalRequest, CommandResponse<LoginFinalResponse>>
{
    public async Task<CommandResponse<LoginFinalResponse>> Handle(LoginFinalRequest request, CancellationToken cancellationToken)
    {
        var usuario = await repositoryUsuario.GetByAsync(true, p => p.Id == request.Id && p.FlagAtivo == true, cancellationToken, "Fornecedores", "Empresas", "Empresas.Fornecedores");
        if (usuario is null)
        {
            AddNotification("Usuário", LoginResources.LoginNaoIdentificado);
            return await Task.FromResult(new CommandResponse<LoginFinalResponse>(this));
        }

        if (!usuario.FlagAtivo)
        {
            AddNotification("Usuário", LoginResources.LoginUsuarioInvativo);
            return await Task.FromResult(new CommandResponse<LoginFinalResponse>(this));
        }

        if (usuario.Senha == null)
        {
            AddNotification("Usuario", LoginResources.LoginCadastroNaoFinalizado);
            return await Task.FromResult(new CommandResponse<LoginFinalResponse>(this));
        }

        if (usuario.Empresas == null || !usuario.Empresas.Any())
        {
            AddNotification("Usuário", LoginResources.LoginUsuarioSemEmpresa);
            return await Task.FromResult(new CommandResponse<LoginFinalResponse>(this));
        }

        if (usuario.CodigoOtp == null || usuario.DataGeracaoCodigoOtp == null)
        {
            AddNotification("Usuario", LoginResources.LoginCodigoOtpNaoGerado);
            return await Task.FromResult(new CommandResponse<LoginFinalResponse>(this));
        }

        if (usuario.CodigoOtp != request.CodigoOtp!.EncryptPassword())
        {
            AddNotification("Usuario", LoginResources.LoginCodigoOtpInvalidoProUsuario);
            return await Task.FromResult(new CommandResponse<LoginFinalResponse>(this));
        }

        string? fornecedorId = null;
        if (usuario.PerfilPermissao == EPerfilUsuario.FornecedorEndUser && usuario.Fornecedores.Count > 0)
        {
            var fornecedorResponse = usuario.Fornecedores.First();
            fornecedorId = fornecedorResponse.Id.ToString();

        }

        var tempoExpiraOtp = Convert.ToInt32(configuration["User:TimeSecondsExpireCodeOtp"]);
        var subtracaoData = DateTime.UtcNow.Subtract(usuario.DataGeracaoCodigoOtp.Value).TotalSeconds;
        if (subtracaoData > tempoExpiraOtp)
        {
            AddNotification("Usuario", LoginResources.LoginCodigoOtpExpiradoProUsuario);
            return await Task.FromResult(new CommandResponse<LoginFinalResponse>(this));
        }

        usuario.CodigoOtp = null;
        usuario.DataGeracaoCodigoOtp = null;
        usuario.LimparChaveCadastro();

        usuario.UltimoLogin = DateTime.UtcNow;
        if (usuario.DataPrimeiroLogin == null)
            usuario.DataPrimeiroLogin = DateTime.UtcNow;

        var usuarioEmpresa = usuario.Empresas.OrderBy(x => x.Nome).First().Id;

        var empresa = await repositoryEmpresa.GetByAsync(true, p => p.Id == usuarioEmpresa);

        if (empresa == null)
        {
            AddNotification("Empresa", EmpresaResources.EmpresaNaoEncontrada);
            return await Task.FromResult(new CommandResponse<LoginFinalResponse>(this));
        }

        bool ehPrimeiroLogin = usuario.UltimoLogin == usuario.DataPrimeiroLogin;

        string empresaId;

        if (usuario.PerfilPermissao == EPerfilUsuario.FornecedorEndUser && usuario.Fornecedores.Count != 0)
        {
            var primeiroFornecedor = usuario.Fornecedores.FirstOrDefault();
            empresaId = primeiroFornecedor?.Empresas.FirstOrDefault()?.Id.ToString() ?? "";
        }
        else
            empresaId = usuario.Empresas.OrderBy(emp => emp.Nome).First().Id.ToString();

        var tokenBuilder = new TokenBuilder(configuration)
            .WithUserId(usuario.Id.ToString())
            .WithUserName(usuario.Nome)
            .WithUserCompany(empresaId)
            .WithProfile(usuario.PerfilPermissao.ToString())
            .WithIdioma(request.Idioma)
            .WithCnpjEmpresa(empresa.Cnpj)
            .WithFlagPrimeiroLogin(ehPrimeiroLogin.ToString());

        if (fornecedorId is not null)
        {
            tokenBuilder.WithFornecedorId(fornecedorId);
        }

        if (empresa is not null && !String.IsNullOrEmpty(empresa.Cnpj))
        {
            tokenBuilder.WithCnpjEmpresa(empresa.Cnpj);
        }

        var token = tokenBuilder.Build();

        return new CommandResponse<LoginFinalResponse>(
            new LoginFinalResponse(usuario.Nome, usuario.Email, token),
            this
        );
    }
}