using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Domain.Commands.TabelaPreco.Listar;
using Bunzl.Domain.Commands.TabelaPreco.Adicionar;
using Bunzl.Domain.Commands.TabelaPreco.Aprovar;
using Bunzl.Domain.Commands.TabelaPreco.Atualizar;
using Bunzl.Domain.Commands.TabelaPreco.Cancelar;
using Bunzl.Domain.Commands.TabelaPreco.ListarProdutos;
using Bunzl.Domain.Commands.TabelaPreco.ObterAguardandoAprovacao;
using Bunzl.Domain.Commands.TabelaPreco.Validar;

namespace Bunzl.Application.Interfaces;

public interface ITabelaPrecoAppService
{
    Task<CommandResponse<DataSourcePageResponse>> Listar(TabelaPrecoListarRequest request);
    Task<CommandResponse<TabelaPrecoObterAguardandoAprovacaoResponse>> ObterAguardandoAprovacao(TabelaPrecoObterAguardandoAprovacaoRequest request);
    Task<CommandResponse<List<TabelaPrecoListarProdutosResponse>>> ObterProdutos(TabelaPrecoListarProdutosRequest request);
    Task<CommandResponse<TabelaPrecoAdicionarResponse>> Adicionar(TabelaPrecoAdicionarRequest request);
    Task<CommandResponse<TabelaPrecoAtualizarResponse>> Atualizar(TabelaPrecoAtualizarRequest request);
    Task<CommandResponse<TabelaPrecoValidarResponse>> Validar(TabelaPrecoValidarRequest request);
    Task<CommandResponse<TabelaPrecoAprovarResponse>> Aprovar(TabelaPrecoAprovarRequest request);
    Task<CommandResponse<TabelaPrecoCancelarResponse>> Cancelar(TabelaPrecoCancelarRequest request);
}