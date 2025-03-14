using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;
using System.Text.Json.Serialization;

namespace Bunzl.Domain.Commands.Fornecedor.DeletarObservacao;

public class FornecedorDeletarObservacaoRequest(Guid fornecedorId, Guid observacaoId) : IRequest<CommandResponse<FornecedorDeletarObservacaoResponse>>
{
    [JsonIgnore] public Guid FornecedorId { get; set; } = fornecedorId;
    [JsonIgnore] public Guid ObservacaoId { get; set; } = observacaoId;
}
