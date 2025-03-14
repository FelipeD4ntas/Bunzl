using Bunzl.Application.Interfaces;
using Bunzl.Domain.Commands.SignUp;
using Bunzl.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bunzl.WebApi.Controllers;

[Route("api/signup")]
[AllowAnonymous]
public class SignUpController(ISignUpAppService signUpService) : BaseApiController
{
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpRequest request)
    {
        var commandResponse = await signUpService.SignUp(request);
        return RespostaCustomizada(commandResponse);
    }
}
