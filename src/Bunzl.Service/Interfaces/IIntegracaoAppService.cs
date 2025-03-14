using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Domain.Commands.Integracao.ObterFornecedores;
using Bunzl.Domain.Commands.Integracao.ObterProdutos;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;

namespace Bunzl.Application.Interfaces;

public interface IIntegracaoAppService
{
    Task<CommandResponse<ObterFornecedoresResponse>> ObterFornecedores(ObterFornecedoresRequest request);
    Task<CommandResponse<DataSourcePageResponse>> ObterProdutos(ObterProdutosRequest request);
}
