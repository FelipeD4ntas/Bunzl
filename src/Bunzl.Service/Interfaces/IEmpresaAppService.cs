using Bunzl.Domain.Commands.Empresa.ObterDataUltimaAtualizacao;
using Bunzl.Domain.Commands.Empresa.ObterPorUsuario;
using Bunzl.Domain.Commands.Empresa.ObterTodos;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;

namespace Bunzl.Application.Interfaces;

public interface IEmpresaAppService
{
    Task<CommandResponse<IEnumerable<EmpresaObterTodosResponse>>> ObterTodos(EmpresaObterTodosRequest request);
    Task<CommandResponse<IEnumerable<EmpresaObterPorUsuarioResponse>>> ObterPorUsuario(EmpresaObterPorUsuarioRequest request);
    Task<CommandResponse<ObterDataUltimaAtualizacaoResponse>> ObterDataUltimaAtualizacao(ObterDataUltimaAtualizacaoRequest request);
}