using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;
using System.Text.Json.Serialization;

namespace Bunzl.Domain.Commands.NegociacaoComercial.DeletarObservacao;

public class NegociacaoComercialDeletarObservacaoRequest(Guid negociacaoComercialId, Guid observacaoId) : IRequest<CommandResponse<NegociacaoComercialDeletarObservacaoResponse>>
{
    [JsonIgnore] public Guid NegociacaoComercialId { get; set; } = negociacaoComercialId;
    [JsonIgnore] public Guid ObservacaoId { get; set; } = observacaoId;
}
