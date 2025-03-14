using Bunzl.Application.Interfaces;
using Bunzl.Domain.Commands.Usuario.Adicionar;
using Bunzl.Domain.Commands.Usuario.Atualizar;
using Bunzl.Domain.Commands.Usuario.AtualizarSenhaTelefone;
using Bunzl.Domain.Commands.Usuario.ConfirmarCadastro;
using Bunzl.Domain.Commands.Usuario.Listar;
using Bunzl.Domain.Commands.Usuario.Obter;
using Bunzl.Domain.Commands.Usuario.ResetSenha;
using Bunzl.Domain.Commands.Usuario.SolicitarResetSenha;
using Bunzl.Domain.Commands.Usuario.VerificarResetSenha;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using Bunzl.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bunzl.WebApi.Controllers;

[Route("api/usuario")]
public class UsuarioController(IUsuarioAppService usuarioAppService, IUsuarioAutenticado usuarioAutenticado) : BaseApiController
{
    [HttpPost("usuarios")]
    public async Task<IActionResult> Listar([FromBody] UsuarioListarRequest request)
    {
        var commandResponse = await usuarioAppService.ListarUsuario(request);
        return RespostaCustomizada(commandResponse);
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar(UsuarioAdicionarRequest request)
    {
        var commandResponse = await usuarioAppService.Adicionar(request);
        return RespostaCustomizada(commandResponse);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar([FromRoute] Guid id, [FromBody] UsuarioAtualizarRequest request)
    {
        request.Id = id;
        var commandResponse = await usuarioAppService.AtualizarUsuario(request);
        return RespostaCustomizada(commandResponse);
    }

    [HttpPut("{id:guid}/ativo")]
    public async Task<IActionResult> Ativar(Guid id)
    {
        var commandResponse = await usuarioAppService.AtivarUsuario(id);
        return RespostaCustomizada(commandResponse);
    }

    [HttpPut("{id:guid}/inativo")]
    public async Task<IActionResult> Inativar(Guid id)
    {
        var commandResponse = await usuarioAppService.InativarUsuario(id);
        return RespostaCustomizada(commandResponse);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Deletar(Guid id)
    {
        var commandResponse = await usuarioAppService.DeletarUsuario(id);
        return RespostaCustomizada(commandResponse);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Obter([FromRoute] Guid id)
    {
        var commandResponse = await usuarioAppService.ObterUsuario(new UsuarioObterRequest(id));
        return RespostaCustomizada(commandResponse);
    }

    [HttpGet("perfil")]
    public async Task<IActionResult> ObterUsuarioLogado()
    {
        var commandResponse = await usuarioAppService.ObterUsuario(new UsuarioObterRequest(usuarioAutenticado.UsuarioId));
        return RespostaCustomizada(commandResponse);
    }

    [HttpPut("perfil")]
    public async Task<IActionResult> AtualizarUsuarioLogado([FromBody] UsuarioAtualizarSenhaTelefoneRequest request)
    {
        request.Id = usuarioAutenticado.UsuarioId;
        var commandResponse = await usuarioAppService.AtualizarSenhaTelefone(request);
        return RespostaCustomizada(commandResponse);
    }

    [AllowAnonymous]
    [HttpPost("confirmar-cadastro")]
    public async Task<IActionResult> ConfirmarCadastro([FromBody] UsuarioConfirmarCadastroRequest request)
    {
        var commandResponse = await usuarioAppService.ConfirmarCadastro(request);
        return RespostaCustomizada(commandResponse);
    }

    [AllowAnonymous]
    [HttpPut("solicitar-reset-senha")]
    public async Task<IActionResult> SolicitarResetSenha(UsuarioSolicitarResetSenhaRequest request)
    {
        var commandResponse = await usuarioAppService.SolicitarResetSenha(request);
        return RespostaCustomizada(commandResponse);
    }

    [AllowAnonymous]
    [HttpPost("reset-senha")]
    public async Task<IActionResult> ResetSenha([FromBody] UsuarioResetSenhaRequest request)
    {
        var commandResponse = await usuarioAppService.ResetSenha(request);
        return RespostaCustomizada(commandResponse);
    }

    [AllowAnonymous]
    [HttpPost("verificar-reset-senha")]
    public async Task<IActionResult> VerificarResetSenha([FromBody] UsuarioVerificarResetSenhaRequest request)
    {
        var commandResponse = await usuarioAppService.VerificarResetSenha(request);
        return RespostaCustomizada(commandResponse);
    }
}