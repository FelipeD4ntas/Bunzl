using Bunzl.Domain.DTOs;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using MediatR;

namespace Bunzl.Domain.Commands.Usuario.Obter
{
    public class UsuarioObterHandler(IRepositoryUsuario repositoryUsuario, IUsuarioAutenticado usuarioAutenticado)
        : Notifiable, IRequestHandler<UsuarioObterRequest, CommandResponse<UsuarioObterResponse>>
    {
        public async Task<CommandResponse<UsuarioObterResponse>> Handle(UsuarioObterRequest request,
            CancellationToken cancellationToken)
        {
            //var usuario =
            //    await repositoryUsuario.GetByAsync(false, (c) => c.Id == request.Id, cancellationToken, p => p.Empresas, p => p.Fornecedores);

            var usuario =
	            await repositoryUsuario.GetByAsync(false, (c) => c.Id == request.Id, cancellationToken, "Empresas", "Fornecedores", "Fornecedores.Empresas");

			if (usuario != null)
            {
                bool ehPrimeiroLogin = usuario.UltimoLogin == usuario.DataPrimeiroLogin;

                var fornecedor = usuario.Fornecedores.FirstOrDefault(p => p.Empresas.Any(p => p.Id == usuarioAutenticado.UsuarioEmpresa));

                if (usuario.Fornecedores.Count > 0 && fornecedor is not null)
                    return new CommandResponse<UsuarioObterResponse>(
                        new UsuarioObterResponse(
                            usuario.Id,
                            usuario.Nome,
                            usuario.Email,
                            usuario.Telefone != null ? usuario.Telefone : string.Empty,
                            usuario.Area != null ? usuario.Area : string.Empty,
                            ehPrimeiroLogin,
                            usuario.PerfilPermissao,
                            usuario.Empresas.Select(e => new EmpresaDto(e.Nome)).ToList(),
                            fornecedor.Id),
                        this);

                return new CommandResponse<UsuarioObterResponse>(
                   new UsuarioObterResponse(
                       usuario.Id,
                       usuario.Nome,
                       usuario.Email,
                       usuario.Telefone != null ? usuario.Telefone : string.Empty,
                       usuario.Area != null ? usuario.Area : string.Empty,
                       ehPrimeiroLogin,
                       usuario.PerfilPermissao,
                       usuario.Empresas.Select(e => new EmpresaDto(e.Nome)).ToList()),
                   this);
            }
               

            AddNotification("Usuario", UsuarioResources.UsuarioNaoEncontrado);
            return new CommandResponse<UsuarioObterResponse>(this);
        }
    }
}