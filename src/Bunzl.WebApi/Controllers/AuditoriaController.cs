using Bunzl.Application.Interfaces;
using Bunzl.Domain.Commands.Auditoria.Listar;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using Bunzl.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Bunzl.WebApi.Controllers;

[Route("api/auditoria")]
public class AuditoriaController(IAuditoriaAppService auditoriaAppService, IUsuarioAutenticado usuarioAutenticado) : BaseApiController
{
    [HttpPost("auditorias")]
    public async Task<IActionResult> Listar([FromBody] AuditoriaListarRequest request)
    {
        var commandResponse = await auditoriaAppService.ListarAuditoria(request);
        return RespostaCustomizada(commandResponse);
    }
}
