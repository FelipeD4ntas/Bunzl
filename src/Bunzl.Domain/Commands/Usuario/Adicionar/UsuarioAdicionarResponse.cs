using Bunzl.Domain.DTOs.Base;
using System.Text.Json.Serialization;

namespace Bunzl.Domain.Commands.Usuario.Adicionar;

public class UsuarioAdicionarResponse : BaseResponseDto
{
    public UsuarioAdicionarResponse(Guid id, string mensagem)
     : base(id, mensagem)
    {
    }
    
    public UsuarioAdicionarResponse(Guid id, string mensagem, string nome, string email, bool jaFoiHomologado, string empresaSolicitante)
      : base(id, mensagem)
    {
        Nome = nome;
        Email = email;
        JaFoiHomologado = jaFoiHomologado;
        EmpresaSolicitante = empresaSolicitante;
    }
    public UsuarioAdicionarResponse(Guid id, string mensagem, string nome, string email, Guid chaveCadastro)
        : base(id, mensagem)
    {
        Nome = nome;
        Email = email;
        ChaveCadastro = chaveCadastro;
    }

    [JsonIgnore]
    public string? Nome { get; set; }

    [JsonIgnore]
    public string? Email { get; set; }

    [JsonIgnore]
    public bool JaFoiHomologado { get; set; }

    [JsonIgnore]
    public string? EmpresaSolicitante { get; set; }

    [JsonIgnore]
    public Guid ChaveCadastro { get; set; }
}