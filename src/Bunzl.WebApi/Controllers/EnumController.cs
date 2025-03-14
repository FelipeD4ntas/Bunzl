using Bunzl.Application.Interfaces;
using Bunzl.Domain.Enumerators;
using Bunzl.Infra.CrossCutting.Extensions;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using Bunzl.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Bunzl.WebApi.Controllers;

[Route("api/enum")]
public class EnumController(IEnumAppService enumAppService, IUsuarioAutenticado usuarioAutenticado) : BaseApiController
{
    [HttpGet("usuario/perfil")]
    public async Task<IActionResult> ListarPerfilUsuarioLogado()
    {
        var result = await enumAppService.ListarPerfilUsuarioLogado(new());
        return RespostaCustomizada(result);
    }

    [HttpGet("usuario/perfil/todos")]
    public IActionResult ListarPerfilUsuario()
    {
        return Ok(EPerfilUsuario.BunzlCorporativoMasterUser.ToDtoList(usuarioAutenticado.Idioma));
    }

    [HttpGet("fornecedor/status/todos")]
    public IActionResult ListarStatusFornecedor()
    {
        return Ok(EStatusFornecedor.Homologado.ToDtoList(usuarioAutenticado.Idioma));
    }

    [HttpGet("negociacao-comercial/status/todos")]
    public IActionResult ListarStatusNegociacaoComercial()
    {
	    return Ok(EStatusNegociacaoComercial.Aceita.ToDtoList(usuarioAutenticado.Idioma));
    }

    [HttpGet("produto/status/todos")]
    public IActionResult ListarStatusProduto()
    {
	    return Ok(EStatusProduto.Homologado.ToDtoList(usuarioAutenticado.Idioma));
    }

    [HttpGet("tabela-preco/status/todos")]
    public IActionResult ListarStatusTabelaPreco()
    {
	    return Ok(EStatusTabelaPreco.Integrada.ToDtoList(usuarioAutenticado.Idioma));
    }

    [HttpGet("tabela-preco-produto/status/todos")]
    public IActionResult ListarStatusTabelaPrecoProduto()
    {
	    return Ok(EStatusTabelaPrecoProduto.Aprovada.ToDtoList(usuarioAutenticado.Idioma));
    }

	[HttpGet("ordem-compra/status/todos")]
	public IActionResult ListarStatusTabelaOrdemDeCompra()
	{
		return Ok(EStatusOrdemDeCompra.AguardandoPi.ToDtoList(usuarioAutenticado.Idioma));
	}

    [HttpGet("anexo/tipo-documento")]
    public IActionResult ListarTipoDocumento()
    {
        return Ok(ETipoDocumento.ImagemProduto.ToDtoList(usuarioAutenticado.Idioma));
    }
}