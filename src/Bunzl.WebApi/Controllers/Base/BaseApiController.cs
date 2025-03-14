using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bunzl.WebApi.Controllers.Base;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    protected IActionResult RespostaCustomizada<T>(CommandResponse<T> command)
        where T : class
    {
        if (command == null)
            return BadRequest();

        if (!command.Sucesso)
            return BadRequest(command);

        return Ok(command);
    }
}
