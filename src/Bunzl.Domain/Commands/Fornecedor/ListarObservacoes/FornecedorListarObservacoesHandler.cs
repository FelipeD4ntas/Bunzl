using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Domain.DTOs;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.ListarObservacoes;

public class FornecedorListarObservacoesHandler(IRepositoryFornecedor repositoryFornecedor, IRepositoryUsuario repositoryUsuario)
    : Notifiable, IRequestHandler<FornecedorListarObservacoesRequest, CommandResponse<DataSourcePageResponse>>
{
    public async Task<CommandResponse<DataSourcePageResponse>> Handle(FornecedorListarObservacoesRequest request, CancellationToken cancellationToken)
    {
        var fornecedor = await repositoryFornecedor.GetByAsync(
            false,
            f => f.Id == request.FornecedorId,
            cancellationToken,
            f => f.FornecedorObservacoes);

        if (fornecedor == null)
        {
            AddNotification("Fornecedor", FornecedorResources.FornecedorNaoEncontrado);

            return new CommandResponse<DataSourcePageResponse>(this);
        }

        var observacoes = fornecedor.FornecedorObservacoes;

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
            Data = observacoes.Select(fo => new FornecedorObservacaoDto(
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