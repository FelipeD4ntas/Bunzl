using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Infra.CrossCutting.Templates.Models.Emails;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.ObterPlanilhaCadastroProduto;

public class ProdutosObterPlanilhaHandler
    : Notifiable, IRequestHandler<ProdutosObterPlanilhaRequest, CommandResponse<ProdutosObterPlanilhaResponse>>
{
    public async Task<CommandResponse<ProdutosObterPlanilhaResponse>> Handle(ProdutosObterPlanilhaRequest request, CancellationToken cancellationToken)
    {
        var assembly = typeof(NovoFornecedorCadastradoModel).Assembly;
        var resourceName = "Bunzl.Infra.CrossCutting.Templates.Views.Produtos.1-PRODUCT_REGISTRATION - SUPPLIER.xlsx";
        var nomeArquivo = "1-PRODUCT_REGISTRATION - SUPPLIER.xlsx";
        var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        using Stream stream = assembly.GetManifestResourceStream(resourceName)!;
        if (stream == null)
        {
            AddNotification("Planilha", FornecedorResources.PlanilhaNaoEncontrado);
            return new CommandResponse<ProdutosObterPlanilhaResponse>(this);
        }

        using MemoryStream ms = new();
        await stream.CopyToAsync(ms, cancellationToken);
        var arquivoBase64 = Convert.ToBase64String(ms.ToArray());

        return new CommandResponse<ProdutosObterPlanilhaResponse>(
            new ProdutosObterPlanilhaResponse(nomeArquivo, mimeType, arquivoBase64), this
        );

    }
}

