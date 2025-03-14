using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using Mapster;
using MediatR;

namespace Bunzl.Domain.Commands.Empresa.ObterTodos;

public class EmpresaObterTodosHandler(IRepositoryEmpresa repositoryEmpresa)
    : Notifiable, IRequestHandler<EmpresaObterTodosRequest, CommandResponse<IEnumerable<EmpresaObterTodosResponse>>>
{
    public async Task<CommandResponse<IEnumerable<EmpresaObterTodosResponse>>> Handle(EmpresaObterTodosRequest request,
        CancellationToken cancellationToken)
    {
        var total = await repositoryEmpresa.CountAsync(cancellationToken);
        if (total == 0)
        {
            AddNotification("Empresa", EmpresaResources.NenhumaEmpresaCadastrada);
            return new CommandResponse<IEnumerable<EmpresaObterTodosResponse>>(this);
        }

        var empresas = await repositoryEmpresa.ListAsync(false, 0, total);
        var empresasMap = empresas.Adapt<List<EmpresaObterTodosResponse>>();

        return new CommandResponse<IEnumerable<EmpresaObterTodosResponse>>(empresasMap, this);
    }
}