using Bunzl.Infra.CrossCutting.Resources;
using FluentValidation;
using FluentValidation.Validators;

namespace Bunzl.Domain.Commands.Fornecedor.Atualizar;

public class FornecedorAtualizarValidator : AbstractValidator<FornecedorAtualizarRequest>
{
    public FornecedorAtualizarValidator()
    {
        RuleFor(p => p.RazaoSocial)
            .NotEmpty()
            .WithMessage(FornecedorResources.RazaoSocialObrigatoria)
            .MaximumLength(300)
            .WithMessage(FornecedorResources.RazaoSocialMaximo300Caracteres);

        RuleFor(p => p.NomeFantasia)
            .NotEmpty()
            .WithMessage(FornecedorResources.NomeFantasiaObrigatorio)
            .MaximumLength(300)
            .WithMessage(FornecedorResources.NomeFantasiaMaximo300Caracteres);

        RuleFor(p => p.Logradouro)
            .NotEmpty()
            .WithMessage(FornecedorResources.LogradouroObrigatorio)
            .MaximumLength(200)
            .WithMessage(FornecedorResources.LogradouroMaximo200Caracteres);
        RuleFor(p => p.Pais)
            .NotEmpty()
            .WithMessage(FornecedorResources.PaisObrigatorio)
            .MaximumLength(50)
            .WithMessage(FornecedorResources.PaisMaximo50Caracteres);

        RuleFor(p => p.EstadoProvincia)
            .NotEmpty()
            .WithMessage(FornecedorResources.EstadoProvinciaObrigatorio)
            .MaximumLength(100)
            .WithMessage(FornecedorResources.EstadoProvinciaMaximo100Caracteres);

        RuleFor(p => p.Cidade)
            .NotEmpty()
            .WithMessage(FornecedorResources.CidadeObrigatoria)
            .MaximumLength(100)
            .WithMessage(FornecedorResources.CidadeMaximo100Caracteres);

        RuleFor(p => p.Contato)
            .MaximumLength(100)
            .WithMessage(FornecedorResources.ContatoMaximo100Caracteres);

        RuleFor(p => p.Website)
            .MaximumLength(200)
            .WithMessage(FornecedorResources.WebsiteMaximo200Caracteres);

        RuleFor(p => p.Email)
            .NotEmpty()
            .WithMessage(FornecedorResources.EmailObrigatorio)
            .EmailAddress(EmailValidationMode.AspNetCoreCompatible)
            .WithMessage(FornecedorResources.EmailInvalido)
            .MaximumLength(200)
            .WithMessage(FornecedorResources.EmailMaximo200Caracteres);

        RuleFor(p => p.TelefoneArea)
            .MaximumLength(5)
            .WithMessage(FornecedorResources.TelefoneAreaMaximo5Caracteres);

        RuleFor(p => p.FabricasAuditadas)
            .NotEmpty()
            .When(p => p.FlagFabricaAuditadaBunzl)
            .WithMessage(FornecedorResources.FabricasAuditadasObrigatorias);

        RuleFor(p => p.NumeroIdentificacaoFiscal)
            .MaximumLength(100)
            .WithMessage(FornecedorResources.CodigoIdentificadorMaximo100Caracteres);

        RuleFor(p => p.MoedaId)
            .NotEmpty()
            .WithMessage(FornecedorResources.MoedaObrigatoria);

        RuleFor(p => p.SiglaFornecedor)
            .MaximumLength(5)
            .WithMessage(FornecedorResources.SiglaFornecedorMaximo5Caracteres);
    }
}