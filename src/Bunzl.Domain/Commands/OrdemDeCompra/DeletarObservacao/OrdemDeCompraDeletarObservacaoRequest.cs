using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;
using System.Text.Json.Serialization;

namespace Bunzl.Domain.Commands.OrdemDeCompra.DeletarObservacao;

public class OrdemDeCompraDeletarObservacaoRequest(Guid negociacaoComercialId, Guid observacaoId) : IRequest<CommandResponse<OrdemDeCompraDeletarObservacaoResponse>>
{
    [JsonIgnore] public Guid OrdemDeCompraId { get; set; } = negociacaoComercialId;
    [JsonIgnore] public Guid ObservacaoId { get; set; } = observacaoId;
}
