using Bunzl.Application.Interfaces;
using Bunzl.Domain.Commands.Empresa.ObterDataUltimaAtualizacao;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using Bunzl.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Bunzl.WebApi.Controllers;

[Route("api/empresa")]
public class EmpresaController(IEmpresaAppService empresaService, IUsuarioAutenticado usuarioAutenticado) : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var commandResponse = await empresaService.ObterTodos(new());
        return RespostaCustomizada(commandResponse);
    }

    [HttpGet("usuario/{usuarioId:guid}")]
    public async Task<IActionResult> ObterPorUsuario(Guid usuarioId)
    {
        var commandResponse = await empresaService.ObterPorUsuario(new(usuarioId));
        return RespostaCustomizada(commandResponse);
    }

    [HttpGet("logado")]
    public async Task<IActionResult> ObterPorUsuarioLogado()
    {
        var commandResponse = await empresaService.ObterPorUsuario(new(usuarioAutenticado.UsuarioId));
        return RespostaCustomizada(commandResponse);
    }

    [HttpGet("ultima-atualizacao/{id:guid}")]
    public async Task<IActionResult> ObterUltimaAtualizacao(Guid id)
    {
        var commandResponse = await empresaService.ObterDataUltimaAtualizacao(new ObterDataUltimaAtualizacaoRequest(id));

        return RespostaCustomizada(commandResponse);
    }
}
