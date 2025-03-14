using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Domain.Commands.Auditoria.Listar;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;


namespace Bunzl.Application.Interfaces;

public interface IAuditoriaAppService
{
    Task<CommandResponse<DataSourcePageResponse>> ListarAuditoria(AuditoriaListarRequest request);
}
