using Bunzl.Application.Interfaces;
using Bunzl.Domain.Commands.Empresa.ObterDataUltimaAtualizacao;
using Bunzl.Domain.Commands.Empresa.ObterPorUsuario;
using Bunzl.Domain.Commands.Empresa.ObterTodos;
using Bunzl.Infra.CrossCutting.IoC.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using MediatR;

namespace Bunzl.Application.Services;

public class EmpresaAppService(ISender mediator) : Notifiable, IEmpresaAppService, IInjectScoped
{
    public async Task<CommandResponse<IEnumerable<EmpresaObterTodosResponse>>> ObterTodos(EmpresaObterTodosRequest request)
    {
        return await mediator.Send(request);
    }

    public async Task<CommandResponse<IEnumerable<EmpresaObterPorUsuarioResponse>>> ObterPorUsuario(EmpresaObterPorUsuarioRequest request)
    {
        return await mediator.Send(request);
    }

    public async Task<CommandResponse<ObterDataUltimaAtualizacaoResponse>> ObterDataUltimaAtualizacao(ObterDataUltimaAtualizacaoRequest request)
    {
        return await mediator.Send(request);
    }
}