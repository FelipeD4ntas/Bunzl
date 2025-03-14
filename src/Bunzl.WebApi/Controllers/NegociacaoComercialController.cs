using Bunzl.Application.Interfaces;
using Bunzl.Domain.Commands.NegociacaoComercial.Adicionar;
using Bunzl.Domain.Commands.NegociacaoComercial.AdicionarAnexo;
using Bunzl.Domain.Commands.NegociacaoComercial.AdicionarObservacao;
using Bunzl.Domain.Commands.NegociacaoComercial.Atualizar;
using Bunzl.Domain.Commands.NegociacaoComercial.AtualizarStatus;
using Bunzl.Domain.Commands.NegociacaoComercial.DeletarAnexo;
using Bunzl.Domain.Commands.NegociacaoComercial.DeletarObservacao;
using Bunzl.Domain.Commands.NegociacaoComercial.Listar;
using Bunzl.Domain.Commands.NegociacaoComercial.ListarAnexos;
using Bunzl.Domain.Commands.NegociacaoComercial.ListarObservacoes;
using Bunzl.Domain.Commands.NegociacaoComercial.Obter;
using Bunzl.Domain.Commands.NegociacaoComercial.ObterAnexoNegociacaoComercial;
using Bunzl.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bunzl.WebApi.Controllers;

[Route("api/negociacao-comercial")]
public class NegociacaoComercialController(INegociacaoComercialAppService negociacaoComercialAppService) : BaseApiController
{
	[HttpPost]
	public async Task<IActionResult> Adicionar(NegociacaoComercialAdicionarRequest request)
	{
		var commandResponse = await negociacaoComercialAppService.Adicionar(request);
		return RespostaCustomizada(commandResponse);
	}

	[HttpPut("{id:guid}")]
	public async Task<IActionResult> Atualizar([FromRoute] Guid id, [FromBody] NegociacaoComercialAtualizarPayload payload)
	{
		var commandResponse = await negociacaoComercialAppService.Atualizar(payload.ToRequest(id));
		return RespostaCustomizada(commandResponse);
	}

    [HttpPut("{id:guid}/Status")]
    public async Task<IActionResult> AtualizarStatus([FromRoute] Guid id, [FromBody] NegociacaoComercialAtualizarStatusPayload payload)
    {
        var commandResponse = await negociacaoComercialAppService.AtualizarStatus(payload.ToRequest(id));
        return RespostaCustomizada(commandResponse);
    }

    [HttpPost("negociacoes-comercial")]
	public async Task<IActionResult> Listar([FromBody] NegociacaoComercialListarRequest request)
	{
		var commandResponse = await negociacaoComercialAppService.ListarNegociacaoComercial(request);
		return RespostaCustomizada(commandResponse);
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> ObterNegociacaoComercial([FromRoute] Guid id)
	{
		var request = new NegociacaoComercialObterRequest(id);
		var commandResponse = await negociacaoComercialAppService.Obter(request);
		return RespostaCustomizada(commandResponse);
	}

	[HttpPost("{id:guid}/anexo")]
	public async Task<IActionResult> AdicionarDocumento([FromRoute] Guid id, [FromForm] NegociacaoComercialAdicionarAnexoPayload payload)
	{
		var commandResponse = await negociacaoComercialAppService.AdicionarAnexo(payload.ToRequest(id));
		return RespostaCustomizada(commandResponse);
	}

    [HttpPost("{id:guid}/anexos")]
    public async Task<IActionResult> ListarAnexos([FromRoute] Guid id, [FromBody] NegociacaoComercialListarAnexosRequest request)
    {
        request.NegociacaoComercialId = id;
        var commandResponse = await negociacaoComercialAppService.ListarAnexos(request);
        return RespostaCustomizada(commandResponse);
    }

    [HttpGet("{id:guid}/anexo/{anexoId:guid}")]
	public async Task<IActionResult> ObterAnexoNegociacaoComercial([FromRoute] Guid id, [FromRoute] Guid anexoId)
	{
		var request = new NegociacaoComercialObterAnexoRequest(id, anexoId);
		var commandResponse = await negociacaoComercialAppService.ObterAnexoNegociacaoComercial(request);
		return RespostaCustomizada(commandResponse);
	}

	[HttpDelete("{id:guid}/anexo/{anexoId:guid}")]
	public async Task<IActionResult> DeletarAnexo([FromRoute] Guid id, [FromRoute] Guid anexoId)
	{
		var request = new NegociacaoComercialDeletarAnexoRequest(id, anexoId);
		var commandResponse = await negociacaoComercialAppService.DeletarAnexo(request);
		return RespostaCustomizada(commandResponse);
	}

	[HttpPost("{id:guid}/observacao")]
	public async Task<IActionResult> AdicionarObservacao([FromRoute] Guid id, [FromForm] NegociacaoComercialAdicionarObservacaoPayload payload)
	{
		var commandResponse = await negociacaoComercialAppService.AdicionarObservacao(payload.ToRequest(id));
		return RespostaCustomizada(commandResponse);
	}

	[HttpDelete("{id:guid}/observacao/{observacaoId:guid}")]
	public async Task<IActionResult> DeletarObservacao([FromRoute] Guid id, [FromRoute] Guid observacaoId)
	{
		var request = new NegociacaoComercialDeletarObservacaoRequest(id, observacaoId);
		var commandResponse = await negociacaoComercialAppService.DeletarObservacao(request);
		return RespostaCustomizada(commandResponse);
	}

	[HttpPost("{id:guid}/observacoes")]
	public async Task<IActionResult> ListarObservacoes([FromRoute] Guid id, [FromBody] NegociacaoComercialListarObservacoesRequest request)
	{
		request.NegociacaoComercialId = id;
		var commandResponse = await negociacaoComercialAppService.ListarObservacoes(request);
		return RespostaCustomizada(commandResponse);
	}
}

