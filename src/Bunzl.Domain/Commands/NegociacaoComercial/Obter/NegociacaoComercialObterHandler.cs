using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using MediatR;
using Bunzl.Domain.DTOs;
using Bunzl.Infra.CrossCutting.Resources;

namespace Bunzl.Domain.Commands.NegociacaoComercial.Obter;

public class NegociacaoComercialObterHandler(
	IRepositoryNegociacaoComercial repositoryNegociacaoComercial, 
	IRepositoryFornecedor repositoryFornecedor,
	IUsuarioAutenticado usuarioAutenticado) 
	: Notifiable, IRequestHandler<NegociacaoComercialObterRequest, CommandResponse<NegociacaoComercialObterResponse>>
{
	public async Task<CommandResponse<NegociacaoComercialObterResponse>> Handle(NegociacaoComercialObterRequest request, CancellationToken cancellationToken)
	{
		var negociacaoComercial = await repositoryNegociacaoComercial.GetByAsync(
			false,
			nc => nc.Id == request.Id,
			cancellationToken,
			nc => nc.Produtos,
			nc => nc.Anexos);

		if (negociacaoComercial == null)
		{
			AddNotification("NegociacaoComercial", NegociacaoComercialResources.NegociacaoComercialNaoEncontrada);
			return new CommandResponse<NegociacaoComercialObterResponse>(this);
		}

		var negociacaoComercialProduto = negociacaoComercial.Produtos.Select(ncp => new NegociacaoComercialProdutoObterDto
		{
			Id = ncp.Id,
			ProdutoId = ncp.ProdutoId,
			CodigoSku = ncp.CodigoSku,
			Descricao = ncp.Descricao,
			Valor = ncp.ValorUnitarioOriginal,
			Quantidade = ncp.Quantidade,
			ValorSugerido = ncp.ValorUnitarioNegociado,
            Observacao = ncp.Observacao,
            DataCriacao = ncp.DataCriacao,
            ValorAlvo = ncp.ValorUnitarioAlvo,
            ValorFinal = ncp.ValorUnitarioFinal
        }).ToList();

		var negociacaoComercialAnexo = negociacaoComercial.Anexos.Select(nca => new NegociacaoComercialAnexoObterDto
		{
			Id = nca.Id,
			Nome = nca.Nome,
			Tipo = nca.Tipo,
			Observacao = nca.Observacao,
			DataCriacao = nca.DataCriacao
		}).ToList();

		var fornecedor = await repositoryFornecedor.GetByAsync(true, c => c.Id == negociacaoComercial.FornecedorId, cancellationToken, p => p.FornecedorObservacoes);
		if (fornecedor == null)
		{
			AddNotification("Fornecedor", FornecedorResources.FornecedorNaoEncontrado);
			return new CommandResponse<NegociacaoComercialObterResponse>(this);
		}

		var response = new NegociacaoComercialObterResponse(
			negociacaoComercial.Id,
			negociacaoComercial.FornecedorId,
			fornecedor.RazaoSocial,
			negociacaoComercial.Titulo,
			negociacaoComercial.DataEntrega,
			negociacaoComercial.DataCriacao,
			negociacaoComercial.CampoAtuacao,
			negociacaoComercial.TermosPagamento,
			negociacaoComercial.ValorTotal,
			negociacaoComercial.Codigo,
			negociacaoComercialProduto,
			negociacaoComercialAnexo);

		return new CommandResponse<NegociacaoComercialObterResponse>(response, this);
	}
}

