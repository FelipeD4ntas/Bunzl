using Bunzl.Application.Interfaces;
using Bunzl.Domain.Commands.Fornecedor.Adicionar;
using Bunzl.Domain.Commands.Fornecedor.AdicionarAnexoProduto;
using Bunzl.Domain.Commands.Fornecedor.AdicionarDocumento;
using Bunzl.Domain.Commands.Fornecedor.AdicionarObservacao;
using Bunzl.Domain.Commands.Fornecedor.AdicionarPlanilhaCadastroProduto;
using Bunzl.Domain.Commands.Fornecedor.AdicionarPlanilhaCadastroProdutoBunzl;
using Bunzl.Domain.Commands.Fornecedor.AdicionarPlanilhaTabelaPreco;
using Bunzl.Domain.Commands.Fornecedor.AdicionarProduto;
using Bunzl.Domain.Commands.Fornecedor.Atualizar;
using Bunzl.Domain.Commands.Fornecedor.AtualizarProduto;
using Bunzl.Domain.Commands.Fornecedor.DeletarAnexoProduto;
using Bunzl.Domain.Commands.Fornecedor.DeletarObservacao;
using Bunzl.Domain.Commands.Fornecedor.DeletarProduto;
using Bunzl.Domain.Commands.Fornecedor.Listar;
using Bunzl.Domain.Commands.Fornecedor.ListarAnexosProduto;
using Bunzl.Domain.Commands.Fornecedor.ListarDocumentos;
using Bunzl.Domain.Commands.Fornecedor.ListarObservacoes;
using Bunzl.Domain.Commands.Fornecedor.ListarProdutos;
using Bunzl.Domain.Commands.Fornecedor.Obter;
using Bunzl.Domain.Commands.Fornecedor.ObterAnexoProduto;
using Bunzl.Domain.Commands.Fornecedor.ObterDocumento;
using Bunzl.Domain.Commands.Fornecedor.ObterProduto;
using Bunzl.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Bunzl.Domain.Commands.Fornecedor.ObterPlanilhaCadastroProdutoBunzl;
using Bunzl.Domain.Commands.Fornecedor.ObterPlanilhaTabelaPreco;
using Bunzl.Domain.Commands.Fornecedor.ListarHomologadoComTabelaPreco;
using Bunzl.Domain.Commands.Fornecedor.ListarProdutosHomologado;

namespace Bunzl.WebApi.Controllers;

[Route("api/fornecedor")]
public class FornecedorController(IFornecedorAppService fornecedorAppService) : BaseApiController
{
    #region Fornecedor
    [HttpPost("fornecedores")]
    public async Task<IActionResult> Listar([FromBody] FornecedorListarRequest request)
    {
        var commandResponse = await fornecedorAppService.ListarFornecedor(request);
        return RespostaCustomizada(commandResponse);
    }
    
    [HttpPost("fornecedores-homologado-com-tabela-preco")]
    public async Task<IActionResult> ListarFornecedorHomologadoComTabelaPreco([FromBody] FornecedorListarHomologadoComTabelaPrecoRequest request)
    {
        var commandResponse = await fornecedorAppService.ListarFornecedorHomologadoComTabelaPreco(request);
        return RespostaCustomizada(commandResponse);
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar(FornecedorAdicionarRequest request)
    {
        var commandResponse = await fornecedorAppService.Adicionar(request);
        return RespostaCustomizada(commandResponse);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar([FromRoute] Guid id, [FromBody] FornecedorAtualizarRequestPayload payload)
    {
        var commandResponse = await fornecedorAppService.AtualizarFornecedor(payload.ToRequest(id));
        return RespostaCustomizada(commandResponse);
    }

    [HttpPut("{id:guid}/ativo")]
    public async Task<IActionResult> Ativar(Guid id)
    {
        var commandResponse = await fornecedorAppService.AtivarFornecedor(id);
        return RespostaCustomizada(commandResponse);
    }

    [HttpPut("{id:guid}/inativo")]
    public async Task<IActionResult> Inativar(Guid id)
    {
        var commandResponse = await fornecedorAppService.InativarFornecedor(id);
        return RespostaCustomizada(commandResponse);
    }

    [HttpPut("{id:guid}/invalidar-portal")]
    public async Task<IActionResult> InvalidarPortal(Guid id)
    {
        var commandResponse = await fornecedorAppService.InvalidarPortalFornecedor(id);
        return RespostaCustomizada(commandResponse);
    }

    [HttpPost("{id:guid}/documentos")]
    public async Task<IActionResult> ListarDocumentos([FromRoute] Guid id, [FromBody] FornecedorListarDocumentosRequest request)
    {
        request.FornecedorId = id;
        var commandResponse = await fornecedorAppService.ListarFornecedorDocumentos(request);
        return RespostaCustomizada(commandResponse);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterFornecedor([FromRoute] Guid id)
    {
        var request = new FornecedorObterRequest(id);
        var commandResponse = await fornecedorAppService.Obter(request);
        return RespostaCustomizada(commandResponse);
    }

    [HttpGet("{id:guid}/documento/{documentoId:guid}")]
    public async Task<IActionResult> ObterDocumento([FromRoute] Guid id, [FromRoute] Guid documentoId)
    {
        var request = new FornecedorObterDocumentoRequest(id, documentoId);
        var commandResponse = await fornecedorAppService.ObterDocumento(request);
        return RespostaCustomizada(commandResponse);
    }

    [HttpPost("{id:guid}/documento")]
    public async Task<IActionResult> AdicionarDocumento([FromRoute] Guid id, [FromForm] FornecedorAdicionarDocumentoPayload payload)
    {
        var commandResponse = await fornecedorAppService.AdicionarDocumento(payload.ToRequest(id));
        return RespostaCustomizada(commandResponse);
    }

    [HttpDelete("{id:guid}/documento/{documentoId:guid}")]
    public async Task<IActionResult> DeletarDocumento([FromRoute] Guid id, [FromRoute] Guid documentoId)
    {
        var commandResponse = await fornecedorAppService.DeletarDocumento(id, documentoId);
        return RespostaCustomizada(commandResponse);
    }

    [HttpPost("{id:guid}/observacao")]
    public async Task<IActionResult> AdicionarObservacao([FromRoute] Guid id, [FromForm] FornecedorAdicionarObservacaoPayload payload)
    {
        var commandResponse = await fornecedorAppService.AdicionarObservacao(payload.ToRequest(id));
        return RespostaCustomizada(commandResponse);
    }

    [HttpPost("{id:guid}/observacoes")]
    public async Task<IActionResult> ListarObservacoes([FromRoute] Guid id, [FromBody] FornecedorListarObservacoesRequest request)
    {
        request.FornecedorId = id;
        var commandResponse = await fornecedorAppService.ListarObservacoes(request);
        return RespostaCustomizada(commandResponse);
    }

    [HttpDelete("{id:guid}/observacao/{observacaoId:guid}")]
    public async Task<IActionResult> DeletarObservacao([FromRoute] Guid id, [FromRoute] Guid observacaoId)
    {
        var request = new FornecedorDeletarObservacaoRequest(id, observacaoId);
        var commandResponse = await fornecedorAppService.DeletarObservacao(request);
        return RespostaCustomizada(commandResponse);
    }
    #endregion

    #region Produto
    [HttpPost("{id:guid}/produto")]
    public async Task<IActionResult> Adicionar([FromRoute] Guid id, FornecedorProdutoAdicionarPayload payload)
    {
        var commandResponse = await fornecedorAppService.AdicionarProduto(payload.ToRequest(id));
        return RespostaCustomizada(commandResponse);
    }

    [HttpPut("{id:guid}/produto/{produtoId:guid}")]
    public async Task<IActionResult> Atualizar([FromRoute] Guid id, [FromRoute] Guid produtoId, [FromBody] FornecedorProdutoAtualizarPayload payload)
    {
        var commandResponse = await fornecedorAppService.AtualizarProduto(payload.ToRequest(id, produtoId));
        return RespostaCustomizada(commandResponse);
    }

    [HttpGet("{id:guid}/produto/{produtoId:guid}")]
    public async Task<IActionResult> ObterProduto([FromRoute] Guid id, [FromRoute] Guid produtoId)
    {
        var request = new FornecedorObterProdutoRequest(id, produtoId);
        var commandResponse = await fornecedorAppService.ObterProduto(request);
        return RespostaCustomizada(commandResponse);
    }

    [HttpDelete("{id:guid}/produto/{produtoId:guid}")]
    public async Task<IActionResult> Deletar([FromRoute] Guid id, [FromRoute] Guid produtoId)
    {
        var request = new FornecedorProdutoDeletarRequest(id, produtoId);
        var commandResponse = await fornecedorAppService.DeletarProduto(request);
        return RespostaCustomizada(commandResponse);
    }

    [HttpPost("{id:guid}/produtos")]
    public async Task<IActionResult> ListarProdutos([FromRoute] Guid id, [FromBody] FornecedorProdutoListarRequest request)
    {
        request.FornecedorId = id;
        var commandResponse = await fornecedorAppService.ListarFornecedorProdutos(request);
        return RespostaCustomizada(commandResponse);
    }

    [HttpGet("{id:guid}/produtos-homologados")]
    public async Task<IActionResult> ListarProdutosHomologados(Guid id)
    {
        var commandResponse = await fornecedorAppService.ListarFornecedorProdutosHomologado(new FornecedorProdutoListarHomologadoRequest(id));
        return RespostaCustomizada(commandResponse);
    }

    [HttpPost("{id:guid}/produto/planilha/cadastro")]
    public async Task<IActionResult> AdicionarPlanilha([FromRoute] Guid id, [FromForm] ProdutosAdicionarPlanilhaPayload payload)
    {
        var commandResponse = await fornecedorAppService.AdicionarPlanilhaProduto(payload.ToRequest(id));
        return RespostaCustomizada(commandResponse);
    }

    [HttpGet("produto/planilha/cadastro")]
    public async Task<IActionResult> ObterPlanilha()
    {
        var commandResponse = await fornecedorAppService.ObterPlanilhaProduto();
        return RespostaCustomizada(commandResponse);
    }

    [HttpGet("{id:guid}/produto/planilha/cadastro/bunzl")]
    public async Task<IActionResult> ObterPlanilhaBunzl([FromRoute] Guid id, [FromQuery] bool semSku)
    {
        var request = new ProdutosObterPlanilhaBunzlRequest(id, semSku);
        var commandResponse = await fornecedorAppService.ObterPlanilhaProdutoBunzl(request);
        return RespostaCustomizada(commandResponse);
    }

    [HttpPost("{id:guid}/produtos/{produtoId:guid}/anexo")]
    public async Task<IActionResult> AdicionarProdutoAnexo([FromRoute] Guid id, [FromRoute] Guid produtoId, [FromForm] FornecedorProdutoAdicionarAnexoPayload payload)
    {
        var commandResponse = await fornecedorAppService.AdicionarProdutoAnexo(payload.ToRequest(id, produtoId));
        return RespostaCustomizada(commandResponse);
    }

    [HttpDelete("{id:guid}/produtos/{produtoId:guid}/anexo/{anexoId:guid}")]
    public async Task<IActionResult> DeletarProdutoAnexo([FromRoute] Guid id, [FromRoute] Guid produtoId, [FromRoute] Guid anexoId)
    {
        var request = new FornecedorProdutoDeletarAnexoRequest(id, produtoId, anexoId);
        var commandResponse = await fornecedorAppService.DeletarProdutoAnexo(request);
        return RespostaCustomizada(commandResponse);
    }

    [HttpPost("{id:guid}/produtos/{produtoId:guid}/anexos")]
    public async Task<IActionResult> ListarProdutoAnexos([FromRoute] Guid id, [FromRoute] Guid produtoId)
    {
        var request = new FornecedorProdutoListarAnexoRequest(id, produtoId);
        var commandResponse = await fornecedorAppService.ListarFornecedorProdutoAnexos(request);
        return RespostaCustomizada(commandResponse);
    }


    [HttpGet("{id:guid}/produtos/{produtoId:guid}/anexo/{anexoId:guid}")]
    public async Task<IActionResult> ObterProdutoAnexo([FromRoute] Guid id, [FromRoute] Guid produtoId, [FromRoute] Guid anexoId)
    {
        var request = new FornecedorProdutoObterAnexoRequest(id, produtoId, anexoId);
        var commandResponse = await fornecedorAppService.ObterFornecedorProdutoAnexo(request);
        return RespostaCustomizada(commandResponse);
    }

    [HttpPost("{id:guid}/produto/planilha/cadastro/bunzl")]
    public async Task<IActionResult> AdicionarPlanilhaBunzl([FromRoute] Guid id, [FromForm] ProdutosAdicionarPlanilhaBunzlPayload payload)
    {
        var commandResponse = await fornecedorAppService.AdicionarPlanilhaProdutoBunzl(payload.ToRequest(id));
        return RespostaCustomizada(commandResponse);
    }
    #endregion

    #region TabelaPreco
    [HttpGet("{id:guid}/produto/planilha/tabela-preco")]
    public async Task<IActionResult> ObterPlanilhaTabelaPreco([FromRoute] Guid id)
    {
        var request = new ProdutosObterPlanilhaTabelaPrecoRequest(id);
        var commandResponse = await fornecedorAppService.ObterPlanilhaTabelaPreco(request);
        return RespostaCustomizada(commandResponse);
    }

    [HttpPost("{id:guid}/produto/planilha/tabela-preco")]
    public async Task<IActionResult> AdicionarPlanilhaTabelaPreco([FromRoute] Guid id, [FromForm] ProdutosAdicionarPlanilhaTabelaPrecoPayload payload)
    {
        var commandResponse = await fornecedorAppService.AdicionarPlanilhaTabelaPreco(payload.ToRequest(id));
        return RespostaCustomizada(commandResponse);
    }

    #endregion
}
