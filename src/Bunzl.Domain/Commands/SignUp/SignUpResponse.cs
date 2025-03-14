using Bunzl.Domain.DTOs.Base;
using System.Text.Json.Serialization;

namespace Bunzl.Domain.Commands.SignUp;

public class SignUpResponse(Guid id, string mensagem, string nome, string email, Guid chaveCadastro) : BaseResponseDto(id, mensagem)
{
    [JsonIgnore]
    public string Nome { get; set; } = nome;

    [JsonIgnore]
    public string Email { get; set; } = email;

    [JsonIgnore]
    public Guid ChaveCadastro { get; set; } = chaveCadastro;
}