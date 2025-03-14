using Bunzl.Application.Interfaces;
using Bunzl.Domain.Commands.Integracao.ObterFornecedores;
using Bunzl.Domain.Commands.Integracao.ObterProdutos;
using Microsoft.AspNetCore.Mvc;

namespace Bunzl.WebApi.Controllers.Base;

[Route("api/integracao")]
public class IntegracaoController(IIntegracaoAppService integracaoAppService) : BaseApiController
{
    [HttpGet("fornecedores/{empresaCnpj}")]
    public async Task<IActionResult> ObterFornecedores(
        [FromRoute] string empresaCnpj,
        [FromQuery] string? codigoFornecedor = null,
        [FromQuery] DateTime? dataAlteracaoInicio = null,
        [FromQuery] DateTime? dataAlteracaoFim = null)
    {
        var request = new ObterFornecedoresRequest(empresaCnpj, codigoFornecedor, dataAlteracaoInicio, dataAlteracaoFim);

        var commandResponse = await integracaoAppService.ObterFornecedores(request);

        return RespostaCustomizada(commandResponse);
    }

    [HttpGet("fornecedores/produtos/{empresaCnpj}")]
    public async Task<IActionResult> ObterProdutos(
        [FromRoute] string empresaCnpj,
        [FromQuery] string? codigoSKU = null,
        [FromQuery] DateTime? dataAlteracaoInicio = null,
        [FromQuery] DateTime? dataAlteracaoFim = null)
    {
        var request = new ObterProdutosRequest(empresaCnpj, codigoSKU, dataAlteracaoInicio, dataAlteracaoFim);

        var commandResponse = await integracaoAppService.ObterProdutos(request);

        return RespostaCustomizada(commandResponse);
    }
}
