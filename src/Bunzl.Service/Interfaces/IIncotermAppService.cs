using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Domain.Commands.Incoterm.Listar;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;

namespace Bunzl.Application.Interfaces;

public interface IIncotermAppService
{
    Task<CommandResponse<DataSourcePageResponse>> Listar(IncotermListarRequest request);
}