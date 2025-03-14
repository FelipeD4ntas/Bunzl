using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Domain.Commands.OrdemDeCompra.Obter;
using Bunzl.Domain.Commands.OrdemDeCompra.AdicionarObservacao;
using Bunzl.Domain.Commands.OrdemDeCompra.ListarObservacoes;
using Bunzl.Domain.Commands.OrdemDeCompra.DeletarObservacao;
using Bunzl.Domain.Commands.OrdemDeCompra.AdicionarAnexo;
using Bunzl.Domain.Commands.OrdemDeCompra.DeletarAnexo;
using Bunzl.Domain.Commands.OrdemDeCompra.Listar;
using Bunzl.Domain.Commands.OrdemDeCompra.ListarAnexos;
using Bunzl.Domain.Commands.OrdemDeCompra.Adicionar;
using Bunzl.Domain.Commands.OrdemDeCompra.AtualizarStatus;
using Bunzl.Domain.Commands.OrdemDeCompra.ObterAnexo;

namespace Bunzl.Application.Interfaces;

public interface IOrdemDeCompraAppService
{
	Task<CommandResponse<OrdemDeCompraAdicionarResponse>> Adicionar(OrdemDeCompraAdicionarRequest request);
	Task<CommandResponse<DataSourcePageResponse>> Listar(OrdemDeCompraListarRequest request);
    Task<CommandResponse<OrdemDeCompraObterResponse>> Obter(OrdemDeCompraObterRequest request);
    Task<CommandResponse<OrdemDeCompraAdicionarObservacaoResponse>> AdicionarObservacao(OrdemDeCompraAdicionarObservacaoRequest request);
    Task<CommandResponse<DataSourcePageResponse>> ListarObservacoes(OrdemDeCompraListarObservacoesRequest request);
    Task<CommandResponse<OrdemDeCompraDeletarObservacaoResponse>> DeletarObservacao(OrdemDeCompraDeletarObservacaoRequest request);
    Task<CommandResponse<OrdemDeCompraAdicionarAnexoResponse>> AdicionarAnexo(OrdemDeCompraAdicionarAnexoRequest request);
    Task<CommandResponse<OrdemDeCompraDeletarAnexoResponse>> DeletarAnexo(OrdemDeCompraDeletarAnexoRequest request);
    Task<CommandResponse<DataSourcePageResponse>> ListarAnexos(OrdemDeCompraListarAnexosRequest request);
    Task<CommandResponse<OrdemDeCompraObterAnexoResponse>> ObterAnexoOrdemDeCompra(OrdemDeCompraObterAnexoRequest request);
    Task<CommandResponse<OrdemDeCompraAtualizarStatusResponse>> AtualizarStatus(OrdemDeCompraAtualizarStatusRequest request);
}

