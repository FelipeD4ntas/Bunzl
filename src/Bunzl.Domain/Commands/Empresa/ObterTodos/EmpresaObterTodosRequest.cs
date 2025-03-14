using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Empresa.ObterTodos;

public class EmpresaObterTodosRequest : IRequest<CommandResponse<IEnumerable<EmpresaObterTodosResponse>>>;
