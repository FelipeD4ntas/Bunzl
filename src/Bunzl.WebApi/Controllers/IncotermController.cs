using Bunzl.Application.Interfaces;
using Bunzl.Domain.Commands.Incoterm.Listar;
using Bunzl.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Bunzl.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncotermController(IIncotermAppService incotermAppService) : BaseApiController
    {
        [HttpPost("listar")]
        public async Task<IActionResult> Listar(IncotermListarRequest request)
        {
            var commandResponse = await incotermAppService.Listar(request);
            return RespostaCustomizada(commandResponse);
        }
    }
}
