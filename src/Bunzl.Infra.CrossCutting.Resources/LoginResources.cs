namespace Bunzl.Infra.CrossCutting.Resources;

public struct LoginResources
{
    public const string EmailObrigatorio = "EMAIL_OBRIGATORIO";
    public const string EmailInvalido = "EMAIL_INVALIDO";
    public const string SenhaMinimo8Caracteres = "SENHA_MINIMO_8_CARACTERES";
    public const string SenhaObrigatoria = "SENHA_OBRIGATORIA";
    public const string LoginInvalido = "USUARIO_INVALIDO";
    public const string LoginNaoIdentificado = "USUARIO_NAO_IDENTIFICADO";
    public const string LoginCadastroNaoFinalizado = "USUARIO_CADASTRO_NAO_FINALIZADO";
    public const string LoginSenhaInvalido = "USUARIO_SENHA_INVALIDO";
    public const string LoginCodigoGerado = "USUARIO_CODIGO_GERADO";
    public const string LoginCodigoOtpInvalido = "USUARIO_CODIGO_OTP_INVALIDO";
    public const string LoginCodigoOtpInvalidoProUsuario = "USUARIO_CODIGO_OTP_INVALIDO_PRO_USUARIO";
    public const string LoginCodigoOtpExpiradoProUsuario = "USUARIO_CODIGO_OTP_EXPIRADO_PRO_USUARIO";
    public const string LoginCodigoOtpNaoGerado = "USUARIO_CODIGO_OTP_NAO_GERADO";
    public const string LoginCodigoOtp6CaracateresNumericos = "USUARIO_CODIGO_OTP_6_CARACTERES_NUMERICOS";
    public const string FalhaEnviarEmailLogin = "FALHA_ENVIAR_EMAIL_LOGIN";
    public const string FalhaEnviarSmsLogin = "FALHA_ENVIAR_SMS_LOGIN";
    public const string LoginExpirado = "LOGIN_EXPIRADA_90_DIAS";

    public const string LoginUsuarioInvativo = "LOGIN_USUARIO_INATIVO";
    public const string LoginUsuarioSemEmpresa = "LOGIN_USUARIO_SEM_EMPRESA";
    public const string LoginEmpresaInvalida = "LOGIN_EMPRESA_INVALIDA";
    public const string LoginEmpresaInvalidaProUsuario = "LOGIN_EMPRESA_INVALIDA_PRO_USUARIO";

}