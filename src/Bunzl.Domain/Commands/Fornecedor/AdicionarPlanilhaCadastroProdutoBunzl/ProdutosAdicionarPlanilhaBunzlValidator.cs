using Bunzl.Infra.CrossCutting.Resources;
using FluentValidation;

namespace Bunzl.Domain.Commands.Fornecedor.AdicionarPlanilhaCadastroProdutoBunzl;

public class ProdutosAdicionarPlanilhaBunzlValidator : AbstractValidator<ProdutosAdicionarPlanilhaBunzlRequest>
{
    public ProdutosAdicionarPlanilhaBunzlValidator()
    {
        RuleFor(p => p.Arquivo.ContentType)
            .NotEmpty()
            .WithMessage(FornecedorResources.PlanilhaTipoObrigatorio)
            .Must(tipo => ValidarTipoDocumento(tipo))
            .WithMessage(FornecedorResources.PlanilhaTipoInvalido);
    }

    private static bool ValidarTipoDocumento(string tipo)
    {
        var tiposPermitidos = new List<string> {
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", // xlsx
			"application/vnd.ms-excel", // xls
		};
        return tiposPermitidos.Contains(tipo);
    }
}
