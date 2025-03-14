using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Bunzl.Domain.Commands.NegociacaoComercial.AdicionarAnexo;

public class NegociacaoComercialAdicionarAnexoRequest(Guid negociacaoComercialId, IFormFile arquivo, string? observacao) : IRequest<CommandResponse<NegociacaoComercialAdicionarAnexoResponse>>
{
    public Guid NegociacaoComercialId { get; set; } = negociacaoComercialId;
    public IFormFile Arquivo { get; set; } = arquivo;
    public string? Observacao { get; set; } = observacao;
}