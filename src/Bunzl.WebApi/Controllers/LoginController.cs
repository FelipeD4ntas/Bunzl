using Bunzl.Application.Interfaces;
using Bunzl.Domain.Commands.Login.LoginAlterarEmpresa;
using Bunzl.Domain.Commands.Login.LoginFinal;
using Bunzl.Domain.Commands.Login.LoginGerarCodigoOtp;
using Bunzl.Domain.Commands.Login.LoginInicial;
using Bunzl.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bunzl.WebApi.Controllers;

[Route("api/login")]
public class LoginController(ILoginAppService loginAppService) : BaseApiController
{

#if DEBUG
    [HttpPost("dev")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginDev()
    {
        var commandResponse = await loginAppService.LoginDev();
        return RespostaCustomizada(commandResponse);
    }
#endif

    [HttpPost("inicial")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginInicial(LoginInicialRequest request)
    {
        var commandResponse = await loginAppService.LoginInicial(request);
        return RespostaCustomizada(commandResponse);
    }

    [HttpPost("codigo-email")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginGerarCodigoEmail(LoginGerarCodigoOtpRequest request)
    {
        var commandResponse = await loginAppService.LoginGerarCodigoEmail(request);
        return RespostaCustomizada(commandResponse);
    }

    [HttpPost("codigo-sms")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginGerarCodigoSms(LoginGerarCodigoOtpRequest request)
    {
        var commandResponse = await loginAppService.LoginGerarCodigoSms(request);
        if (commandResponse.Sucesso)
            return Ok(commandResponse);

        return BadRequest(commandResponse);
    }

    [HttpPost("final")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginFinal(LoginFinalRequest request)
    {
        var commandResponse = await loginAppService.LoginFinal(request);
        return RespostaCustomizada(commandResponse);
    }

    [HttpPost("alterar-empresa")]
    public async Task<IActionResult> LoginAlterarEmpresa(LoginAlterarEmpresaRequest request)
    {
        var commandResponse = await loginAppService.LoginAlterarEmpresa(request);
        return RespostaCustomizada(commandResponse);
    }
}
