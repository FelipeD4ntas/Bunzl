using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Bunzl.Domain.Commands.OrdemDeCompra.AdicionarAnexo;

public class OrdemDeCompraAdicionarAnexoRequest(Guid ordemDeCompraId, IFormFile arquivo) : IRequest<CommandResponse<OrdemDeCompraAdicionarAnexoResponse>>
{
    public Guid OrdemDeCompraId { get; set; } = ordemDeCompraId;
    public IFormFile Arquivo { get; set; } = arquivo;
}