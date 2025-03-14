using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Infra.CrossCutting.Templates.Models.Emails;
using ClosedXML.Excel;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.ObterPlanilhaTabelaPreco;

public class ProdutosObterPlanilhaTabelaPrecoHandler(IRepositoryFornecedor repositoryFornecedor)
    : Notifiable, IRequestHandler<ProdutosObterPlanilhaTabelaPrecoRequest, CommandResponse<ProdutosObterPlanilhaTabelaPrecoResponse>>
{
    public async Task<CommandResponse<ProdutosObterPlanilhaTabelaPrecoResponse>> Handle(ProdutosObterPlanilhaTabelaPrecoRequest request, CancellationToken cancellationToken)
    {
        var fornecedor = await repositoryFornecedor.GetByAsync(false, f => f.Id == request.FornecedorId, true, cancellationToken, p => p.Moeda, p => p.FornecedorProdutos);
        if (fornecedor == null)
        {
            AddNotification("Fornecedor", FornecedorResources.FornecedorNaoEncontrado);
            return new CommandResponse<ProdutosObterPlanilhaTabelaPrecoResponse>(this);
        }

        var assembly = typeof(NovoFornecedorCadastradoModel).Assembly;
        var resourceName = "Bunzl.Infra.CrossCutting.Templates.Views.Produtos.3-PRICE_LIST - SUPPLIER.xlsx";
        var nomeArquivo = "3-PRICE_LIST - SUPPLIER.xlsx";
        var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        using Stream stream = assembly.GetManifestResourceStream(resourceName)!;
        if (stream == null)
        {
            AddNotification("Planilha", FornecedorResources.PlanilhaNaoEncontrado);
            return new CommandResponse<ProdutosObterPlanilhaTabelaPrecoResponse>(this);
        }

        using var workbook = new XLWorkbook(stream);
        var worksheet = workbook.Worksheet(1);

        int currentRow = 7;
		foreach (var produto in fornecedor.FornecedorProdutos
			         .Where(p => !string.IsNullOrEmpty(p.CodigoSku))
			         .OrderBy(p => p.CodigoFornecedor))
		{
            worksheet.Cell(currentRow, 2).Value = produto.Id.ToString();
            worksheet.Cell(currentRow, 3).Value = fornecedor.CodigoERP;
            worksheet.Cell(currentRow, 4).Value = fornecedor.RazaoSocial;
            worksheet.Cell(currentRow, 5).Value = produto.CodigoSku;
            worksheet.Cell(currentRow, 6).Value = produto.DescricaoCompletaBunzl;
            worksheet.Cell(currentRow, 7).Value = produto.CodigoFornecedor;
            worksheet.Cell(currentRow, 8).Value = produto.DescricaoCompletaFornecedor;
            worksheet.Cell(currentRow, 9).Value = produto.UnidadeMedidaFornecedorPreco;
            worksheet.Cell(currentRow, 10).Value = fornecedor.Moeda.Sigla;
            worksheet.Cell(currentRow, 11).Value = produto.Preco;
            worksheet.Cell(currentRow, 11).Style.NumberFormat.Format = "#,##0.000000";

            currentRow++;
        }

        worksheet.Column(2).Hide();

        using MemoryStream ms = new();
        workbook.SaveAs(ms);
        var arquivoBase64 = Convert.ToBase64String(ms.ToArray());

        return new CommandResponse<ProdutosObterPlanilhaTabelaPrecoResponse>(
            new ProdutosObterPlanilhaTabelaPrecoResponse(nomeArquivo, mimeType, arquivoBase64), this
        );
    }
}

