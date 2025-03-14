using Bunzl.Core.Domain.Interfaces.ExternalService;
using Bunzl.Domain.Commands.TabelaPreco.Adicionar;
using Bunzl.Domain.DTOs.TabelaPreco;
using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using MediatR;

namespace Bunzl.Domain.Commands.TabelaPreco.Atualizar;

public class TabelaPrecoAtualizarHandler(
    IRepositoryTabelaPreco repositoryTabelaPreco,
    IRepositoryEmpresa repositoryEmpresa,
	IRepositoryUsuario repositoryUsuario,
	IUsuarioAutenticado usuarioAutenticado) : Notifiable, IRequestHandler<TabelaPrecoAtualizarRequest, CommandResponse<TabelaPrecoAtualizarResponse>>
{
    public async Task<CommandResponse<TabelaPrecoAtualizarResponse>> Handle(TabelaPrecoAtualizarRequest request, CancellationToken cancellationToken)
    {
		var usuario = await repositoryUsuario.GetByAsync(true, u => u.Id == usuarioAutenticado.UsuarioId, false, cancellationToken, u => u.Fornecedores);
		if (usuario == null)
		{
			AddNotification("Usuarios", UsuarioResources.UsuarioNaoEncontrado);
			return new CommandResponse<TabelaPrecoAtualizarResponse>(this);
		}

		var empresa = await repositoryEmpresa.GetByAsync(true, u => u.Id == usuarioAutenticado.UsuarioEmpresa, false, cancellationToken, e => e.Usuarios);
		if (empresa == null)
		{
			AddNotification("Empresa", EmpresaResources.EmpresaNaoEncontrada);
			return new CommandResponse<TabelaPrecoAtualizarResponse>(this);
		}

		var usuariosEmpresaEmail = empresa.Usuarios.Where(u => u.PerfilPermissao != Enumerators.EPerfilUsuario.BunzlCorporativoMasterUser && u.PerfilPermissao != EPerfilUsuario.FornecedorEndUser).ToList();

		var tabelaPreco = await repositoryTabelaPreco.GetByAsync(true, p => p.Id == request.Id, cancellationToken, p => p.Produtos);

        if (tabelaPreco is null)
        {
            AddNotification("TabelaPreco", TabelaPrecoResources.TabelaPrecoNaoEncontrada);
            return new CommandResponse<TabelaPrecoAtualizarResponse>(this);
        }

        List<TabelaPrecoProdutoComErrosDto> listaTabelaPrecoProdutoComErrosDto = [];

        request.Produtos.ForEach(tppDto =>
        {
            var tabelaPrecoProduto = tabelaPreco.Produtos.FirstOrDefault(p => p.Id == tppDto.Id);
            if (tabelaPrecoProduto is not null)
                tabelaPrecoProduto.Status = tppDto.Status;
            else
                listaTabelaPrecoProdutoComErrosDto.Add(new TabelaPrecoProdutoComErrosDto( string.Format(TabelaPrecoResources.TabelaPrecoProdutoNaoEncontrado, tppDto.Id)));
        });

        if (IsInvalid())
            return new CommandResponse<TabelaPrecoAtualizarResponse>(this);

        tabelaPreco.DataFimVigencia = request.DataFimVigencia;
        repositoryTabelaPreco.Update(tabelaPreco);

		if (usuarioAutenticado.Permissoes == EPerfilUsuario.FornecedorEndUser.ToString())
		{
			var fornecedor = usuario.Fornecedores.FirstOrDefault();
			if (fornecedor == null)
			{
				AddNotification("Fornecedor", FornecedorResources.FornecedorNaoEncontrado);
				return new CommandResponse<TabelaPrecoAtualizarResponse>(this);
			}

			return new CommandResponse<TabelaPrecoAtualizarResponse>(new TabelaPrecoAtualizarResponse(tabelaPreco.Id, TabelaPrecoResources.TabelaPrecoAtualizadaComSucesso, listaTabelaPrecoProdutoComErrosDto, usuariosEmpresaEmail, fornecedor.NomeFantasia), this);
		}

		return new CommandResponse<TabelaPrecoAtualizarResponse>(new TabelaPrecoAtualizarResponse(tabelaPreco.Id, TabelaPrecoResources.TabelaPrecoAtualizadaComSucesso, listaTabelaPrecoProdutoComErrosDto), this);
    }
}