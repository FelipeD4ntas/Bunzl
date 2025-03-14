using Bunzl.Domain.Commands.Fornecedor.Adicionar;
using Bunzl.Domain.Entities;
using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using MediatR;

namespace Bunzl.Domain.Commands.TabelaPreco.Adicionar;

public class TabelaPrecoAdicionarHandler(
    IPublisher mediator,
    IRepositoryTabelaPreco repositoryTabelaPreco,
    IRepositoryFornecedor repositoryFornecedor,
	IRepositoryEmpresa repositoryEmpresa,
	IRepositoryUsuario repositoryUsuario,
	IUsuarioAutenticado usuarioAutenticado
    ) : Notifiable, IRequestHandler<TabelaPrecoAdicionarRequest, CommandResponse<TabelaPrecoAdicionarResponse>>
{
    public async Task<CommandResponse<TabelaPrecoAdicionarResponse>> Handle(TabelaPrecoAdicionarRequest request, CancellationToken cancellationToken)
    {
		var usuario = await repositoryUsuario.GetByAsync(true, u => u.Id == usuarioAutenticado.UsuarioId, false, cancellationToken, u => u.Fornecedores);
		if (usuario == null)
		{
			AddNotification("Usuarios", UsuarioResources.UsuarioNaoEncontrado);
			return new CommandResponse<TabelaPrecoAdicionarResponse>(this);
		}

		var empresa = await repositoryEmpresa.GetByAsync(true, u => u.Id == usuarioAutenticado.UsuarioEmpresa, false, cancellationToken, e => e.Usuarios);
		if (empresa == null)
		{
			AddNotification("Empresa", EmpresaResources.EmpresaNaoEncontrada);
			return new CommandResponse<TabelaPrecoAdicionarResponse>(this);
		}

		var usuariosEmpresaEmail = empresa.Usuarios.Where(u => u.PerfilPermissao != Enumerators.EPerfilUsuario.BunzlCorporativoMasterUser && u.PerfilPermissao != EPerfilUsuario.FornecedorEndUser).ToList();

		if (request.Produtos.Count == 0)
        {
            AddNotification("Produtos", FornecedorResources.TabelaPrecoSemProdutos);
            return new CommandResponse<TabelaPrecoAdicionarResponse>(this);
        }

        if (!await repositoryFornecedor.ExistsAsync(p => p.Id == request.FornecedorId, cancellationToken: cancellationToken))
        {
            AddNotification("Fornecedor", FornecedorResources.FornecedorNaoEncontrado);
            return new CommandResponse<TabelaPrecoAdicionarResponse>(this);
        }

        if (IsInvalid())
            return await Task.FromResult(new CommandResponse<TabelaPrecoAdicionarResponse>(this));

        var tabelaPrecoAguardandoAprovacao = await repositoryTabelaPreco.GetByAsync(true, p => p.FornecedorId == request.FornecedorId && p.EmpresaId == usuarioAutenticado.UsuarioEmpresa && p.Status == EStatusTabelaPreco.AguardandoAprovacao, cancellationToken: cancellationToken, p => p.Produtos);
        if (tabelaPrecoAguardandoAprovacao is not null)
        {
            tabelaPrecoAguardandoAprovacao.Status = EStatusTabelaPreco.Cancelada;
            tabelaPrecoAguardandoAprovacao.FlagExpirada = true;
            foreach (var produto in tabelaPrecoAguardandoAprovacao.Produtos)
            {
                produto.Status = EStatusTabelaPrecoProduto.Cancelada;
            }
            repositoryTabelaPreco.Update(tabelaPrecoAguardandoAprovacao);
        }

        var tabelaPrecoId = Guid.NewGuid();
        var tabelaPreco = new Entities.TabelaPreco(
            tabelaPrecoId,
            usuarioAutenticado.UsuarioEmpresa,
            request.FornecedorId,
            request.Produtos.Select(p => new TabelaPrecoProduto
            {
                ProdutoId = p.ProdutoId,
                UltimoPrecoPraticado = p.UltimoPrecoPraticado,
                NovoPreco = p.NovoPreco,
                Status = EStatusTabelaPrecoProduto.AguardandoAprovacao,
                TabelaPrecoId = tabelaPrecoId
            }).ToList());

        await repositoryTabelaPreco.AddAsync(tabelaPreco, cancellationToken);

        await mediator.Publish(new AuditoriaAdicionarInput(tabelaPreco.Id, TabelasResources.FornecedorProduto, FornecedorResources.ConfirmacaoTabelaPrecoFeitaComSucesso, ETipoAuditoria.Modificado));

        if (usuarioAutenticado.Permissoes == EPerfilUsuario.FornecedorEndUser.ToString())
		{
            var fornecedor = usuario.Fornecedores.FirstOrDefault();
			if (fornecedor == null)
			{
				AddNotification("Fornecedor", FornecedorResources.FornecedorNaoEncontrado);
				return new CommandResponse<TabelaPrecoAdicionarResponse>(this);
			}
			return new CommandResponse<TabelaPrecoAdicionarResponse>(new TabelaPrecoAdicionarResponse(tabelaPreco.Id, FornecedorResources.ConfirmacaoTabelaPrecoFeitaComSucesso, usuariosEmpresaEmail, fornecedor.NomeFantasia), this);
		}

		return new CommandResponse<TabelaPrecoAdicionarResponse>(new TabelaPrecoAdicionarResponse(tabelaPreco.Id, FornecedorResources.ConfirmacaoTabelaPrecoFeitaComSucesso), this);
    }
}