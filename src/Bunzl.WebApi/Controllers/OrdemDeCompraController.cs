using Bunzl.Application.Interfaces;
using Bunzl.Domain.Commands.OrdemDeCompra.Adicionar;
using Bunzl.Domain.Commands.OrdemDeCompra.AdicionarAnexo;
using Bunzl.Domain.Commands.OrdemDeCompra.AdicionarObservacao;
using Bunzl.Domain.Commands.OrdemDeCompra.AtualizarStatus;
using Bunzl.Domain.Commands.OrdemDeCompra.DeletarAnexo;
using Bunzl.Domain.Commands.OrdemDeCompra.DeletarObservacao;
using Bunzl.Domain.Commands.OrdemDeCompra.Listar;
using Bunzl.Domain.Commands.OrdemDeCompra.ListarAnexos;
using Bunzl.Domain.Commands.OrdemDeCompra.ListarObservacoes;
using Bunzl.Domain.Commands.OrdemDeCompra.Obter;
using Bunzl.Domain.Commands.OrdemDeCompra.ObterAnexo;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.WebApi.Controllers.Base;
using Bunzl.WebApi.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using DevExpress.DataAccess.EntityFramework;

namespace Bunzl.WebApi.Controllers;

[Route("api/ordem-de-compra")]
public class OrdemDeCompraController(IOrdemDeCompraAppService ordemDeCompraAppService) : BaseApiController
{
	[HttpPost]
	public async Task<IActionResult> Adicionar(OrdemDeCompraAdicionarRequest request)
	{
		var commandResponse = await ordemDeCompraAppService.Adicionar(request);
		return RespostaCustomizada(commandResponse);
	}

	[HttpPost("ordens-de-compra")]
    public async Task<IActionResult> Listar([FromBody] OrdemDeCompraListarRequest request)
    {
        var commandResponse = await ordemDeCompraAppService.Listar(request);
        return RespostaCustomizada(commandResponse);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterOrdemDeCompra([FromRoute] Guid id)
    {
        var request = new OrdemDeCompraObterRequest(id);
        var commandResponse = await ordemDeCompraAppService.Obter(request);
        return RespostaCustomizada(commandResponse);
    }

    [HttpPost("{id:guid}/observacao")]
    public async Task<IActionResult> AdicionarObservacao([FromRoute] Guid id, [FromForm] OrdemDeCompraAdicionarObservacaoPayload payload)
    {
        var commandResponse = await ordemDeCompraAppService.AdicionarObservacao(payload.ToRequest(id));
        return RespostaCustomizada(commandResponse);
    }

    [HttpPost("{id:guid}/observacoes")]
    public async Task<IActionResult> ListarObservacoes([FromRoute] Guid id, [FromBody] OrdemDeCompraListarObservacoesRequest request)
    {
        request.OrdemDeCompraId = id;
        var commandResponse = await ordemDeCompraAppService.ListarObservacoes(request);
        return RespostaCustomizada(commandResponse);
    }

    [HttpDelete("{id:guid}/observacao/{observacaoId:guid}")]
    public async Task<IActionResult> DeletarObservacao([FromRoute] Guid id, [FromRoute] Guid observacaoId)
    {
        var request = new OrdemDeCompraDeletarObservacaoRequest(id, observacaoId);
        var commandResponse = await ordemDeCompraAppService.DeletarObservacao(request);
        return RespostaCustomizada(commandResponse);
    }

    [HttpPost("{id:guid}/anexo")]
    public async Task<IActionResult> AdicionarDocumento([FromRoute] Guid id, [FromForm] OrdemDeCompraAdicionarAnexoPayload payload)
    {
        var commandResponse = await ordemDeCompraAppService.AdicionarAnexo(payload.ToRequest(id));
        return RespostaCustomizada(commandResponse);
    }

    [HttpDelete("{id:guid}/anexo/{anexoId:guid}")]
    public async Task<IActionResult> DeletarAnexo([FromRoute] Guid id, [FromRoute] Guid anexoId)
    {
        var request = new OrdemDeCompraDeletarAnexoRequest(id, anexoId);
        var commandResponse = await ordemDeCompraAppService.DeletarAnexo(request);
        return RespostaCustomizada(commandResponse);
    }

    [HttpPost("{id:guid}/anexos")]
    public async Task<IActionResult> ListarAnexos([FromRoute] Guid id, [FromBody] OrdemDeCompraListarAnexosRequest request)
    {
        request.OrdemDeCompraId = id;
        var commandResponse = await ordemDeCompraAppService.ListarAnexos(request);
        return RespostaCustomizada(commandResponse);
    }

    [HttpGet("obter-anexo/{anexoId:guid}")]
    public async Task<IActionResult> ObterAnexoOrdemDeCompra(Guid anexoId)
    {
        var request = new OrdemDeCompraObterAnexoRequest(anexoId);
        var commandResponse = await ordemDeCompraAppService.ObterAnexoOrdemDeCompra(request);
        return RespostaCustomizada(commandResponse);
    }

    [HttpPut("atualizar-status")]
    public async Task<IActionResult> AtualizarStatusOrdemDeCompra([FromBody] OrdemDeCompraAtualizarStatusRequest request)
    {
        var commandResponse = await ordemDeCompraAppService.AtualizarStatus(request);
        return RespostaCustomizada(commandResponse);
	}

    [HttpGet("{id:guid}/relatorio")]
    [ApiExplorerSettings(IgnoreApi = false)]
    [AllowAnonymous]
    public async Task<IActionResult> Relatorio(Guid id)
    {
        var diretorio = Path.Combine(Directory.GetCurrentDirectory(), "PDFs");
        if (!Directory.Exists(diretorio))
            Directory.CreateDirectory(diretorio);

        var report = new ReportOrdemDeCompra();
        report.Parameters["Id"].Value = id.ToString();

        byte[] fileArray;
        using (var ms = new MemoryStream())
        {
            await report.ExportToPdfAsync(ms, new DevExpress.XtraPrinting.PdfExportOptions { ShowPrintDialogOnOpen = false });
            var file = new FileStream(Path.Combine(diretorio, $"{id}.pdf"), FileMode.Create, FileAccess.Write);
            ms.WriteTo(file);
            fileArray = ms.ToArray();
        }

        var msReport = new MemoryStream(fileArray);

        var result = new FileStreamResult(msReport, MediaTypeNames.Application.Pdf)
        {
            FileDownloadName = $"PO_{id}.pdf"
        };

        return result;
    }
}

