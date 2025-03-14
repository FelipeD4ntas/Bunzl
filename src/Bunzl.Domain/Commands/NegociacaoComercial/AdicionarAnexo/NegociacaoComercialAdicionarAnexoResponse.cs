using Bunzl.Domain.DTOs.Base;
using Bunzl.Domain.Entities;
using System.Text.Json.Serialization;

namespace Bunzl.Domain.Commands.NegociacaoComercial.AdicionarAnexo;

public class NegociacaoComercialAdicionarAnexoResponse(Guid id, string mensagem, NegociacaoComercialAnexo anexo) : BaseResponseDto(id, mensagem)
{
    [JsonIgnore]
    public NegociacaoComercialAnexo Anexo { get; set; } = anexo;
    public Guid AnexoId { get; set; }
}