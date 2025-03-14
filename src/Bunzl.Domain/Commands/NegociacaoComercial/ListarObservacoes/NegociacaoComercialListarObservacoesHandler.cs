using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Domain.DTOs;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.NegociacaoComercial.ListarObservacoes;

public class NegociacaoComercialListarObservacoesHandler(IRepositoryNegociacaoComercial repositoryNegociacaoComercial, IRepositoryUsuario repositoryUsuario)
    : Notifiable, IRequestHandler<NegociacaoComercialListarObservacoesRequest, CommandResponse<DataSourcePageResponse>>
{
    public async Task<CommandResponse<DataSourcePageResponse>> Handle(NegociacaoComercialListarObservacoesRequest request, CancellationToken cancellationToken)
    {
        var negociacaoComercial = await repositoryNegociacaoComercial.GetByAsync(
            false,
            f => f.Id == request.NegociacaoComercialId,
            cancellationToken,
            f => f.NegociacaoComercialObservacoes);

        if (negociacaoComercial == null)
        {
            AddNotification("NegociacaoComercial", NegociacaoComercialResources.NegociacaoComercialNaoEncontrada);

            return new CommandResponse<DataSourcePageResponse>(this);
        }

        var observacoes = negociacaoComercial.NegociacaoComercialObservacoes;

        var usuarioIds = observacoes
            .Select(fo => fo.UsuarioCriacao)
            .Distinct()
            .ToList();

        var usuarios = await repositoryUsuario.ListAsync(
            false,
            u => usuarioIds.Contains(u.Id),
            true);

        var usuarioDict = usuarios.ToDictionary(u => u.Id);

        var dataSourcePageResponse = new DataSourcePageResponse
        {
            Data = observacoes.Select(fo => new NegociacaoComercialObservacoesDto(
                   fo.Id,
                   fo.UsuarioCriacao,
                   usuarioDict.TryGetValue(fo.UsuarioCriacao, out var usuario) ? usuario.Nome : "",
                   fo.DataCriacao,
                   fo.Observacao)).ToList(),
            TotalCount = observacoes.Count,
            Summary = null
        };

        return new CommandResponse<DataSourcePageResponse>(dataSourcePageResponse, this);
    }
}