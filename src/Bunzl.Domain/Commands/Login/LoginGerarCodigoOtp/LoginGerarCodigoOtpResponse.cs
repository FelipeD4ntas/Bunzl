using Bunzl.Domain.DTOs.Base;
using System.Text.Json.Serialization;

namespace Bunzl.Domain.Commands.Login.LoginGerarCodigoOtp;

public class LoginGerarCodigoOtpResponse(Guid id, string mensagem, string nome, string email, string area, string telefone, string codigoOtp) : BaseResponseDto(id, mensagem)
{
    [JsonIgnore]
    public string Nome { get; set; } = nome;

    [JsonIgnore]
    public string Email { get; set; } = email;

    [JsonIgnore]
    public string Area { get; set; } = area;

    [JsonIgnore]
    public string Telefone { get; set; } = telefone;

    [JsonIgnore]
    public string CodigoOtp { get; set; } = codigoOtp;
}