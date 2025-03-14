using Bunzl.Domain.Commands.Login.LoginAlterarEmpresa;
using Bunzl.Domain.Commands.Login.LoginDev;
using Bunzl.Domain.Commands.Login.LoginFinal;
using Bunzl.Domain.Commands.Login.LoginGerarCodigoOtp;
using Bunzl.Domain.Commands.Login.LoginInicial;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;

namespace Bunzl.Application.Interfaces;

public interface ILoginAppService
{
    Task<CommandResponse<LoginDevResponse>> LoginDev();
    Task<CommandResponse<LoginInicialResponse>> LoginInicial(LoginInicialRequest request);
    Task<CommandResponse<LoginGerarCodigoOtpResponse>> LoginGerarCodigoEmail(LoginGerarCodigoOtpRequest request);
    Task<CommandResponse<LoginGerarCodigoOtpResponse>> LoginGerarCodigoSms(LoginGerarCodigoOtpRequest request);
    Task<CommandResponse<LoginFinalResponse>> LoginFinal(LoginFinalRequest request);
    Task<CommandResponse<LoginAlterarEmpresaResponse>> LoginAlterarEmpresa(LoginAlterarEmpresaRequest request);
}