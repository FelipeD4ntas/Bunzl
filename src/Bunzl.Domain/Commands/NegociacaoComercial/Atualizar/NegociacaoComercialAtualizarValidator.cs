using Bunzl.Domain.Commands.NegociacaoComercial.Adicionar;
using Bunzl.Infra.CrossCutting.Resources;
using FluentValidation;

namespace Bunzl.Domain.Commands.NegociacaoComercial.Atualizar;

public class NegociacaoComercialAdicionarValidator : AbstractValidator<NegociacaoComercialAdicionarRequest>
{
    public NegociacaoComercialAdicionarValidator()
    {

        RuleFor(x => x.DataEntrega)
            .NotEmpty()
            .WithMessage(NegociacaoComercialResources.DataEntregaObrigatorio);

        RuleFor(x => x.TermosPagamento)
            .NotEmpty()
            .WithMessage(NegociacaoComercialResources.TermosPagamentoObrigatorio);

        RuleForEach(x => x.Anexos)
	        .Must(anexo => ValidarTipoDocumento(anexo.Tipo))
	        .WithMessage(NegociacaoComercialResources.AnexoTipoInvalido);
	}

    private bool ValidarTipoDocumento(string tipo)
    {
	    var tiposPermitidos = new List<string> {
		    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", // xlsx
		    "application/vnd.ms-excel", // xls
		    "application/msword", // doc
		    "application/vnd.openxmlformats-officedocument.wordprocessingml.document", // docx
		    "application/pdf", // pdf
		    "text/csv", // csv
		    "text/plain",  // txt
		    "image/jpeg", // jpeg
		    "image/jpg", // jpg
		    "image/png" // png
	    };

	    return tiposPermitidos.Contains(tipo);
    }
}

