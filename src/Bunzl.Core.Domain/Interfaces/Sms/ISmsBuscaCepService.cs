﻿using Bunzl.Core.Domain.DTOs.Sms;

namespace Bunzl.Core.Domain.Interfaces.Sms;

public interface ISmsBuscaCepService
{
    Task<EnviarSmsDto> EnviarSms(string area, string telefone, string texto);
    Task<EnviarSmsDto> EnviarSmsCodigoOtp(string area, string telefone, string codigoOtp);
}
