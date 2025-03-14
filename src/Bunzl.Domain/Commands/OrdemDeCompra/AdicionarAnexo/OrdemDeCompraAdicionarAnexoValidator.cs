using Bunzl.Infra.CrossCutting.Resources;
using FluentValidation;

namespace Bunzl.Domain.Commands.OrdemDeCompra.AdicionarAnexo;

public class OrdemDeCompraAdicionarAnexoValidator : AbstractValidator<OrdemDeCompraAdicionarAnexoRequest>
{
    public OrdemDeCompraAdicionarAnexoValidator()
    {
        RuleFor(p => p.Arquivo.ContentType)
            .NotEmpty()
            .WithMessage(FornecedorResources.DocumentoTipoObrigatorio)
            .MaximumLength(100)
            .WithMessage(FornecedorResources.DocumentoTipoMaximo100Caracteres)
            .Must(tipo => ValidarTipoDocumento(tipo))
            .WithMessage(FornecedorResources.DocumentoTipoInvalido);
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