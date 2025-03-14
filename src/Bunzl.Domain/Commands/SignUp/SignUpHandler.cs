using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.SignUp;

public class SignUpHandler(IRepositoryUsuario repositoryUsuario, IRepositoryEmpresa repositoryEmpresa) : Notifiable, IRequestHandler<SignUpRequest, CommandResponse<SignUpResponse>>
{
    public async Task<CommandResponse<SignUpResponse>> Handle(SignUpRequest request, CancellationToken cancellationToken)
    {
        var perfilCorporativoJaExiste = await repositoryUsuario.ExistsAsync(p => p.PerfilPermissao == Enumerators.EPerfilUsuario.BunzlCorporativoMasterUser, false, cancellationToken);
        if (perfilCorporativoJaExiste)
            AddNotification("", UsuarioResources.UsuarioCorporativoJaExiste);

        var emailExists = await repositoryUsuario.ExistsAsync(p => p.Email == request.Email, false, cancellationToken);
        if (emailExists)
            AddNotification("Email", UsuarioResources.EmailDuplicado);

        if (IsInvalid())
            return await Task.FromResult(new CommandResponse<SignUpResponse>(this));

        var nomesEmpresas = new List<string> { "Danny", "Talge", "Volk" };
        var empresas = nomesEmpresas.Select(
            nome => new Entities.Empresa
            {
                Nome = nome
            }).ToList();

        await repositoryEmpresa.AddCollectionAsync(empresas, cancellationToken);

        var usuario = new Entities.Usuario
        {
            Email = request.Email,
            Nome = request.Nome,
            PerfilPermissao = Enumerators.EPerfilUsuario.BunzlCorporativoMasterUser,
        };

        usuario.RelacionarEmpresas(empresas);

        usuario.GerarChaveCadastro();
        if (usuario.ChaveCadastro == null)
        {
            AddNotification("ChaveCadastro", UsuarioResources.FalhaGerarChaveCadastro);
            return new CommandResponse<SignUpResponse>(this);
        }

        await repositoryUsuario.AddAsync(usuario, cancellationToken);

        return new CommandResponse<SignUpResponse>(
            new SignUpResponse(
                usuario.Id,
                SignUpResources.SignUpAdicionadoComSucesso,
                usuario.Nome,
                usuario.Email,
                usuario.ChaveCadastro.Value),
            this);
    }
}