using Bunzl.Application.Interfaces;
using Bunzl.Domain.Commands.TabelaPreco.Adicionar;
using Bunzl.Domain.Commands.TabelaPreco.Aprovar;
using Bunzl.Domain.Commands.TabelaPreco.Atualizar;
using Bunzl.Domain.Commands.TabelaPreco.Cancelar;
using Bunzl.Domain.Commands.TabelaPreco.Listar;
using Bunzl.Domain.Commands.TabelaPreco.ListarProdutos;
using Bunzl.Domain.Commands.TabelaPreco.ObterAguardandoAprovacao;
using Bunzl.Domain.Commands.TabelaPreco.Validar;
using Bunzl.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Bunzl.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TabelaPrecoController(ITabelaPrecoAppService tabelaPrecoAppService) : BaseApiController
    {
        [HttpPost("listar")]
        public async Task<IActionResult> Listar(TabelaPrecoListarRequest request)
        {
            var commandResponse = await tabelaPrecoAppService.Listar(request);
            return RespostaCustomizada(commandResponse);
        }

        [HttpGet("fornecedor/{fornecedorId:guid}/aguardando-aprovacao")]
        public async Task<IActionResult> ObterAguardandoAprovacao(Guid fornecedorId)
        {
            var commandResponse = await tabelaPrecoAppService.ObterAguardandoAprovacao(new TabelaPrecoObterAguardandoAprovacaoRequest(fornecedorId));
            return RespostaCustomizada(commandResponse);
        }

        [HttpGet("{id:guid}/listar-produtos")]
        public async Task<IActionResult> ListarProdutos(Guid id)
        {
            var commandResponse = await tabelaPrecoAppService.ObterProdutos(new TabelaPrecoListarProdutosRequest(id));
            return RespostaCustomizada(commandResponse);
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> Adicionar(TabelaPrecoAdicionarRequest request)
        {
            var commandResponse = await tabelaPrecoAppService.Adicionar(request);
            return RespostaCustomizada(commandResponse);            
        }


        [HttpPut("atualizar")]
        public async Task<IActionResult> Atualizar(TabelaPrecoAtualizarRequest request)
        {
            var commandResponse = await tabelaPrecoAppService.Atualizar(request);
            return RespostaCustomizada(commandResponse);
        }

        [HttpPut("validar")]
        public async Task<IActionResult> Validar(TabelaPrecoValidarRequest request)
        {
            var commandResponse = await tabelaPrecoAppService.Validar(request);
            return RespostaCustomizada(commandResponse);
        }

        [HttpPut("aprovar")]
        public async Task<IActionResult> Aprovar(TabelaPrecoAprovarRequest request)
        {
            var commandResponse = await tabelaPrecoAppService.Aprovar(request);
            return RespostaCustomizada(commandResponse);
        }

        [HttpPut("cancelar")]
        public async Task<IActionResult> Cancelar(TabelaPrecoCancelarRequest request)
        {
            var commandResponse = await tabelaPrecoAppService.Cancelar(request);
            return RespostaCustomizada(commandResponse);
        }
    }
}
