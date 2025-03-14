using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Domain.Commands.Moeda.Adicionar;
using Bunzl.Domain.Commands.Moeda.Listar;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;

namespace Bunzl.Application.Interfaces;

public interface IMoedaAppService
{
    Task<CommandResponse<DataSourcePageResponse>> Listar(MoedaListarRequest request);
    Task<CommandResponse<MoedaAdicionarResponse>> Adicionar(MoedaAdicionarRequest request);
}