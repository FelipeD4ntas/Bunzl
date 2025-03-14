using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Domain.DTOs;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.OrdemDeCompra.ListarObservacoes;

public class OrdemDeCompraListarObservacoesHandler(IRepositoryOrdemDeCompra repositoryOrdemDeCompra, IRepositoryUsuario repositoryUsuario)
    : Notifiable, IRequestHandler<OrdemDeCompraListarObservacoesRequest, CommandResponse<DataSourcePageResponse>>
{
    public async Task<CommandResponse<DataSourcePageResponse>> Handle(OrdemDeCompraListarObservacoesRequest request, CancellationToken cancellationToken)
    {
        var ordemDeCompra = await repositoryOrdemDeCompra.GetByAsync(
            false,
            f => f.Id == request.OrdemDeCompraId,
            cancellationToken,
            f => f.Observacoes);

        if (ordemDeCompra == null)
        {
            AddNotification("OrdemDeCompra", OrdemDeCompraResources.OrdemDeCompraNaoEncontrada);

            return new CommandResponse<DataSourcePageResponse>(this);
        }

        var observacoes = ordemDeCompra.Observacoes;

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
            Data = observacoes.Select(fo => new OrdemDeCompraObservacaoListarDto(
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