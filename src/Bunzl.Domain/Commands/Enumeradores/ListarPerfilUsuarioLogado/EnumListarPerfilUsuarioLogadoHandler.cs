using Bunzl.Core.Domain.Enumerators;
using Bunzl.Domain.Enumerators;
using Bunzl.Infra.CrossCutting.Extensions;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using MediatR;
using prmToolkit.EnumExtension;

namespace Bunzl.Domain.Commands.Enumeradores.ListarPerfilUsuarioLogado;

public class EnumListarPerfilUsuarioLogadoHandler(IUsuarioAutenticado usuarioAutenticado) : Notifiable, IRequestHandler<EnumListarPerfilUsuarioLogadoRequest, CommandResponse<IEnumerable<EnumDto>>>
{
    public async Task<CommandResponse<IEnumerable<EnumDto>>> Handle(EnumListarPerfilUsuarioLogadoRequest request, CancellationToken cancellationToken)
    {
        var perfilUsuarioAtual = usuarioAutenticado.Permissoes.ToEnum<EPerfilUsuario>();
        var todosPerfil = EPerfilUsuario.BunzlCorporativoMasterUser.ToDtoList(usuarioAutenticado.Idioma);

        if (perfilUsuarioAtual == EPerfilUsuario.BunzlCorporativoMasterUser)
            return await Task.FromResult(new CommandResponse<IEnumerable<EnumDto>>(todosPerfil, this));

        var perfisFiltrados = EPerfilUsuario.BunzlCorporativoMasterUser.ToDtoList(usuarioAutenticado.Idioma, EPerfilUsuario.BunzlCorporativoMasterUser);
        return await Task.FromResult(new CommandResponse<IEnumerable<EnumDto>>(perfisFiltrados, this));
    }
}
