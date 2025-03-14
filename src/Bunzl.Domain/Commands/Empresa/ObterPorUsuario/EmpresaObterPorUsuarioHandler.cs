using Bunzl.Domain.Commands.Usuario.Obter;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using Mapster;
using MediatR;

namespace Bunzl.Domain.Commands.Empresa.ObterPorUsuario;

public class EmpresaObterPorUsuarioHandler(
	IRepositoryUsuario repositoryUsuario,
	IRepositoryEmpresa repositoryEmpresa)
	: Notifiable, IRequestHandler<EmpresaObterPorUsuarioRequest, CommandResponse<IEnumerable<EmpresaObterPorUsuarioResponse>>>
{
	public async Task<CommandResponse<IEnumerable<EmpresaObterPorUsuarioResponse>>> Handle(EmpresaObterPorUsuarioRequest request,
		CancellationToken cancellationToken)
	{
		var usuario = await repositoryUsuario.GetByAsync(false, (c) => c.Id == request.UsuarioId, cancellationToken, "Fornecedores", "Fornecedores.Empresas");

		if (usuario is null)
		{
			AddNotification("Usuario", UsuarioResources.UsuarioNaoEncontrado);
			return new CommandResponse<IEnumerable<EmpresaObterPorUsuarioResponse>>(this);
		}

		if (!await repositoryEmpresa.ExistsAsync(x => x.Usuarios.Any(x => x.Id == request.UsuarioId), false, cancellationToken))
		{
			AddNotification("Empresa", EmpresaResources.NenhumaEmpresaVinculadoUsuario);
			return new CommandResponse<IEnumerable<EmpresaObterPorUsuarioResponse>>(this);
		}

		var empresas = await repositoryEmpresa.ListAsync(false, x => x.Usuarios.Any(x => x.Id == request.UsuarioId));
		var empresasMap = empresas.Select(empresa =>
		{
			var fornecedorLogadoId = usuario.Fornecedores
												.Where(fornecedor => fornecedor.Empresas.Any(emp => emp.Id == empresa.Id))
												.Select(fornecedor => fornecedor.Id)
												.FirstOrDefault();

			return new EmpresaObterPorUsuarioResponse
			{
				Id = empresa.Id,
				Nome = empresa.Nome,
				FornecedorLogadoId = fornecedorLogadoId,
				DataUltimaAtualizacao = empresa.DataUltimaAtualizacao,
				DataUltimaAtualizacaoOrdemDeCompra = empresa.DataUltimaAtualizacaoOrdemDeCompra
			};
		}).ToList();

		return new CommandResponse<IEnumerable<EmpresaObterPorUsuarioResponse>>(empresasMap, this);
	}
}