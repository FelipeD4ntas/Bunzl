using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using MediatR;
using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.Resources;
using prmToolkit.EnumExtension;
using Bunzl.Core.Domain.Interfaces.ExternalService;
using Bunzl.Domain.Entities;

namespace Bunzl.Domain.Commands.NegociacaoComercial.AtualizarStatus;

public class NegociacaoComercialAtualizarStatusHandler(
    IPublisher mediator,
    IRepositoryNegociacaoComercial repositoryNegociacaoComercial,
    IRepositoryTabelaPreco repositoryTabelaPreco,
    IUsuarioAutenticado usuarioAutenticado)
    : Notifiable, IRequestHandler<NegociacaoComercialAtualizarStatusRequest, CommandResponse<NegociacaoComercialAtualizarStatusResponse>>
{
    public async Task<CommandResponse<NegociacaoComercialAtualizarStatusResponse>> Handle(NegociacaoComercialAtualizarStatusRequest request, CancellationToken cancellationToken)
    {
        var perfilUsuarioAtual = usuarioAutenticado.Permissoes.ToEnum<EPerfilUsuario>();
        if (perfilUsuarioAtual == EPerfilUsuario.FornecedorEndUser && request.Status != EStatusNegociacaoComercial.Aceita)
            AddNotification("Usuario", NegociacaoComercialResources.FornecedorNaoPodeAlteratStatusParaRecusadaOuAceitaEIntegrada);

        var negociacaoComercial = await repositoryNegociacaoComercial.GetByAsync(true, p => p.Id == request.Id, cancellationToken, p => p.Fornecedor, p => p.NegociacaoComercialObservacoes, p => p.Produtos, p => p.Anexos);
        if (negociacaoComercial is null)
            AddNotification("NegociacaoComercial", NegociacaoComercialResources.NegociacaoComercialNaoEncontrada);

        var tabelaPreco = await repositoryTabelaPreco.GetByAsync(true, tp => tp.FornecedorId == negociacaoComercial!.FornecedorId && tp.Status == EStatusTabelaPreco.Integrada && tp.FlagExpirada == false && tp.EmpresaId == usuarioAutenticado.UsuarioEmpresa, false, cancellationToken, tp => tp.Produtos);
        if (tabelaPreco == null)
            AddNotification("TabelaPreco", TabelaPrecoResources.TabelaPrecoNaoEncontrada);

        if (IsInvalid())
            return new CommandResponse<NegociacaoComercialAtualizarStatusResponse>(this);

        negociacaoComercial!.AtualizarStatus(request.Status);

        if (request.Status == EStatusNegociacaoComercial.Aceita)
        {
	        negociacaoComercial.AtualizarStatus(request.Status);
	        repositoryNegociacaoComercial.Update(negociacaoComercial);

	        await mediator.Publish(new AuditoriaAdicionarInput(negociacaoComercial.Id, TabelasResources.NegociacaoComercial, "Atualizado", ETipoAuditoria.Modificado), cancellationToken);

	        return new CommandResponse<NegociacaoComercialAtualizarStatusResponse>(new NegociacaoComercialAtualizarStatusResponse(negociacaoComercial.Id, NegociacaoComercialResources.NegociacaoComercialAtualizadaComSucesso), this);
        }

		if (request.Status == EStatusNegociacaoComercial.Concluida)
        {
            if (request.Status == EStatusNegociacaoComercial.Aceita && usuarioAutenticado.Permissoes != EPerfilUsuario.FornecedorEndUser.ToString())
	            AddNotification("NegociacaoComercial", NegociacaoComercialResources.SomenteFornecedorPodeMudarStatusParaAceita);

            if (request.Status != EStatusNegociacaoComercial.Aceita && usuarioAutenticado.Permissoes == EPerfilUsuario.FornecedorEndUser.ToString())
	            AddNotification("NegociacaoComercial", NegociacaoComercialResources.FornecedorNaoPodeAlteratStatusParaRecusadaOuAceitaEIntegrada);

            if (IsInvalid())
	            return new CommandResponse<NegociacaoComercialAtualizarStatusResponse>(this);

            var idTabelaPreco = Guid.NewGuid();
            var novaTabelaPreco = new Entities.TabelaPreco(
                idTabelaPreco,
                tabelaPreco.EmpresaId,
                tabelaPreco.FornecedorId,
                tabelaPreco.Produtos.Select(prod => new TabelaPrecoProduto
                {
                    TabelaPrecoId = idTabelaPreco,
                    ProdutoId = prod.ProdutoId,
                    UltimoPrecoPraticado = prod.NovoPreco, //prod.UltimoPrecoPraticado,
                    NovoPreco = prod.NovoPreco,
                }).ToList());

            //Atualizando Preço no DTO da Tabela de Preço
            negociacaoComercial.Produtos.ForEach(prod =>
            {
                var produtoTabelaPreco = novaTabelaPreco.Produtos.FirstOrDefault(p => p.ProdutoId == prod.ProdutoId);
                if (produtoTabelaPreco != null)
                    produtoTabelaPreco.NovoPreco = prod.ValorUnitarioNegociado;
            });

            await repositoryTabelaPreco.AddAsync(novaTabelaPreco, cancellationToken);

            repositoryNegociacaoComercial.Update(negociacaoComercial);

            await mediator.Publish(new AuditoriaAdicionarInput(negociacaoComercial.Id, TabelasResources.NegociacaoComercial, "Atualizado", ETipoAuditoria.Modificado), cancellationToken);

            return new CommandResponse<NegociacaoComercialAtualizarStatusResponse>(new NegociacaoComercialAtualizarStatusResponse(negociacaoComercial.Id, NegociacaoComercialResources.NegociacaoComercialAtualizadaComSucesso, true), this);
		}

        repositoryNegociacaoComercial.Update(negociacaoComercial);

        await mediator.Publish(new AuditoriaAdicionarInput(negociacaoComercial.Id, TabelasResources.NegociacaoComercial, "Atualizado", ETipoAuditoria.Modificado), cancellationToken);

        return new CommandResponse<NegociacaoComercialAtualizarStatusResponse>(new NegociacaoComercialAtualizarStatusResponse(negociacaoComercial.Id, NegociacaoComercialResources.NegociacaoComercialAtualizadaComSucesso), this);
    }
}

