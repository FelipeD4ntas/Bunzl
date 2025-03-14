using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using Mapster;
using MediatR;

namespace Bunzl.Domain.Commands.Moeda.Adicionar;

public class MoedaAdicionarHandler(
    IRepositoryMoeda repositoryMoeda) : Notifiable, IRequestHandler<MoedaAdicionarRequest, CommandResponse<MoedaAdicionarResponse>>
{
    public async Task<CommandResponse<MoedaAdicionarResponse>> Handle(MoedaAdicionarRequest request, CancellationToken cancellationToken)
    {
        var moeda = request.Adapt<Entities.Moeda>();
        await repositoryMoeda.AddAsync(moeda, cancellationToken);

        return new CommandResponse<MoedaAdicionarResponse>(new MoedaAdicionarResponse(moeda.Id, MoedaResources.MoedaAdicionadaComSucesso), this);
    }
}