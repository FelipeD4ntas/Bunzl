using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.Auditoria.Listar;

public class AuditoriaListarHandler(IRepositoryAuditoria repositoryAuditoria, IRepositoryUsuario repositoryUsuario)
    : Notifiable, IRequestHandler<AuditoriaListarRequest, CommandResponse<DataSourcePageResponse>>
{
    public async Task<CommandResponse<DataSourcePageResponse>> Handle(AuditoriaListarRequest request, CancellationToken cancellationToken)
    {
        var dataSourcePageResponse = await repositoryAuditoria
            .ListDevExpressAsync<AuditoriaListarResponse>(
            false,
            request,
            a => a.EntidadeNome == request.EntidadeNome && a.EntidadeId == request.EntidadeId);

        var data = dataSourcePageResponse.Data as IEnumerable<AuditoriaListarResponse>;

        if (data == null)
        {
	        AddNotification("Auditoria", AuditoriaResources.NenhumaAuditoriaEncontrada);
			return new CommandResponse<DataSourcePageResponse>(this);
        }

        var usuarioIds = data
            .Select(fo => fo.UsuarioCriacao)
            .Distinct()
            .ToList();

        var usuarios = await repositoryUsuario.ListAsync(
            false,
            u => usuarioIds.Contains(u.Id),
            true);


        var usuarioDict = usuarios.ToDictionary(u => u.Id);

        dataSourcePageResponse.Data = data.Select(ad => new AuditoriaListarResponse(
                   ad.Id,
                   ad.EntidadeId,
                   ad.EntidadeNome,
                   ad.Funcao,
                   ad.UsuarioCriacao,
                   usuarioNome: usuarioDict.TryGetValue(ad.UsuarioCriacao, out var usuario) ? usuario.Nome : "",
                   ad.DataCriacao,
                   ad.TipoAuditoria)).ToList();


        return new CommandResponse<DataSourcePageResponse>(dataSourcePageResponse, this);
    }
}
