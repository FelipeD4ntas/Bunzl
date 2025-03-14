using Bunzl.Domain.Commands.NegociacaoComercial.Adicionar;
using Bunzl.Domain.Commands.NegociacaoComercial.Listar;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Domain.Commands.NegociacaoComercial.Obter;
using Bunzl.Domain.Commands.NegociacaoComercial.ObterAnexoNegociacaoComercial;
using Bunzl.Domain.Commands.NegociacaoComercial.AdicionarObservacao;
using Bunzl.Domain.Commands.NegociacaoComercial.DeletarObservacao;
using Bunzl.Domain.Commands.NegociacaoComercial.ListarObservacoes;
using Bunzl.Domain.Commands.NegociacaoComercial.AdicionarAnexo;
using Bunzl.Domain.Commands.NegociacaoComercial.DeletarAnexo;
using Bunzl.Domain.Commands.NegociacaoComercial.Atualizar;
using Bunzl.Domain.Commands.NegociacaoComercial.AtualizarStatus;
using Bunzl.Domain.Commands.NegociacaoComercial.ListarAnexos;

namespace Bunzl.Application.Interfaces;

public interface INegociacaoComercialAppService
{
    Task<CommandResponse<NegociacaoComercialAdicionarResponse>> Adicionar(NegociacaoComercialAdicionarRequest request);
    Task<CommandResponse<NegociacaoComercialAtualizarResponse>> Atualizar(NegociacaoComercialAtualizarRequest request);
    Task<CommandResponse<NegociacaoComercialAtualizarStatusResponse>> AtualizarStatus(NegociacaoComercialAtualizarStatusRequest request);
    Task<CommandResponse<DataSourcePageResponse>> ListarNegociacaoComercial(NegociacaoComercialListarRequest request);
    Task<CommandResponse<NegociacaoComercialObterResponse>> Obter(NegociacaoComercialObterRequest request);
    Task<CommandResponse<NegociacaoComercialAdicionarAnexoResponse>> AdicionarAnexo(NegociacaoComercialAdicionarAnexoRequest request);
    Task<CommandResponse<DataSourcePageResponse>> ListarAnexos(NegociacaoComercialListarAnexosRequest request);
    Task<CommandResponse<NegociacaoComercialObterAnexoResponse>> ObterAnexoNegociacaoComercial(NegociacaoComercialObterAnexoRequest request);
	Task<CommandResponse<NegociacaoComercialDeletarAnexoResponse>> DeletarAnexo(NegociacaoComercialDeletarAnexoRequest request);
	Task<CommandResponse<NegociacaoComercialAdicionarObservacaoResponse>> AdicionarObservacao(NegociacaoComercialAdicionarObservacaoRequest request);
    Task<CommandResponse<NegociacaoComercialDeletarObservacaoResponse>> DeletarObservacao(NegociacaoComercialDeletarObservacaoRequest request);
    Task<CommandResponse<DataSourcePageResponse>> ListarObservacoes(NegociacaoComercialListarObservacoesRequest request);
}

