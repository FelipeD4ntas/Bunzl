using Bunzl.Application.Interfaces;
using Bunzl.Domain.Commands.Moeda.Adicionar;
using Bunzl.Domain.Commands.Moeda.Listar;
using Bunzl.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Bunzl.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoedaController(IMoedaAppService moedaAppService) : BaseApiController
    {
        [HttpPost("listar")]
        public async Task<IActionResult> Listar(MoedaListarRequest request)
        {
            var commandResponse = await moedaAppService.Listar(request);
            return RespostaCustomizada(commandResponse);
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> Adicionar(MoedaAdicionarRequest request)
        {
            var commandResponse = await moedaAppService.Adicionar(request);
            return RespostaCustomizada(commandResponse);            
        }
    }
}
