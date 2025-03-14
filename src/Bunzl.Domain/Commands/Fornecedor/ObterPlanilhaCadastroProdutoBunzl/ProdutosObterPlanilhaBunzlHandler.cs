using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Infra.CrossCutting.Templates.Models.Emails;
using ClosedXML.Excel;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.ObterPlanilhaCadastroProdutoBunzl;

public class ProdutosObterPlanilhaBunzlHandler(IRepositoryFornecedor repositoryFornecedor)
    : Notifiable, IRequestHandler<ProdutosObterPlanilhaBunzlRequest, CommandResponse<ProdutosObterPlanilhaBunzlResponse>>
{
    public async Task<CommandResponse<ProdutosObterPlanilhaBunzlResponse>> Handle(ProdutosObterPlanilhaBunzlRequest request, CancellationToken cancellationToken)
    {
        var fornecedor = await repositoryFornecedor.GetByAsync(true, f => f.Id == request.FornecedorId, cancellationToken, p => p.FornecedorProdutos);
        if (fornecedor == null)
        {
            AddNotification("Fornecedor", FornecedorResources.FornecedorNaoEncontrado);
            return new CommandResponse<ProdutosObterPlanilhaBunzlResponse>(this);
        }

        var assembly = typeof(NovoFornecedorCadastradoModel).Assembly;
        var resourceName = "Bunzl.Infra.CrossCutting.Templates.Views.Produtos.2-PRODUCT_ASSOCIATION - BUNZL x SUPPLIER.xlsx";
        var nomeArquivo = "2-PRODUCT_ASSOCIATION - BUNZL x SUPPLIER.xlsx";
        var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        using Stream stream = assembly.GetManifestResourceStream(resourceName)!;
        if (stream == null)
        {
            AddNotification("Planilha", FornecedorResources.PlanilhaNaoEncontrado);
            return new CommandResponse<ProdutosObterPlanilhaBunzlResponse>(this);
        }

        using var workbook = new XLWorkbook(stream);
        var worksheet = workbook.Worksheet(1);

        var currentRow = 7;
        if (request.SemSku)
        {
            foreach (var produto in fornecedor.FornecedorProdutos.Where(p => string.IsNullOrEmpty(p.CodigoSku)).OrderBy(p => p.CodigoFornecedor))
            {
                worksheet.Cell(currentRow, 2).Value = produto.Id.ToString();
                worksheet.Cell(currentRow, 3).Value = produto.CodigoFornecedor;
                worksheet.Cell(currentRow, 4).Value = produto.DescricaoCompletaFornecedor;
                worksheet.Cell(currentRow, 5).Value = produto.Cor;
                worksheet.Cell(currentRow, 6).Value = produto.Tamanho;
                worksheet.Cell(currentRow, 7).Value = produto.UnidadeMedidaFornecedorPreco;
                worksheet.Cell(currentRow, 8).Value = produto.Preco;
                worksheet.Cell(currentRow, 8).Style.NumberFormat.Format = "#,##0.000000";
                worksheet.Cell(currentRow, 9).Value = "";
                worksheet.Cell(currentRow, 10).Value = produto.Status == EStatusProduto.Homologado ? "Sim" : "Não";

                currentRow++;
            }

            worksheet.Column(2).Hide();
        }
        else
        {
            foreach (var produto in fornecedor.FornecedorProdutos)
            {
                worksheet.Cell(currentRow, 2).Value = produto.Id.ToString();
                worksheet.Cell(currentRow, 3).Value = produto.CodigoFornecedor;
                worksheet.Cell(currentRow, 4).Value = produto.DescricaoCompletaFornecedor;
                worksheet.Cell(currentRow, 5).Value = produto.Cor;
                worksheet.Cell(currentRow, 6).Value = produto.Tamanho;
                worksheet.Cell(currentRow, 7).Value = produto.UnidadeMedidaFornecedorPreco;
                worksheet.Cell(currentRow, 8).Value = produto.Preco;
                worksheet.Cell(currentRow, 8).Style.NumberFormat.Format = "#,##0.000000";
                worksheet.Cell(currentRow, 9).Value = produto.CodigoSku ?? "";
                worksheet.Cell(currentRow, 10).Value = produto.Status == EStatusProduto.Homologado ? "Sim" : "Não";

                currentRow++;
            }

            worksheet.Column(2).Hide();
        }

        using MemoryStream ms = new();
        workbook.SaveAs(ms);
        var arquivoBase64 = Convert.ToBase64String(ms.ToArray());

        return new CommandResponse<ProdutosObterPlanilhaBunzlResponse>(
            new ProdutosObterPlanilhaBunzlResponse(nomeArquivo, mimeType, arquivoBase64), this
        );
    }
}

