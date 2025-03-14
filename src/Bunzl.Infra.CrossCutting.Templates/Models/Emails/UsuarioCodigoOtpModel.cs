using Bunzl.Infra.CrossCutting.Templates.Enums;
using Bunzl.Infra.CrossCutting.Templates.Models.Base;

namespace Bunzl.Infra.CrossCutting.Templates.Models.Emails;

public class UsuarioCodigoOtpModel(string nome, string codigoOtp, string destinatario, string assunto, string titulo)
    : TemplateEmailModelBase(TipoTemplateEmailEnum.UsuarioCodigoOtp, destinatario, assunto, titulo)
{
    public string Nome { get; } = nome;
    public string CodigoOtp { get; } = codigoOtp;
}
