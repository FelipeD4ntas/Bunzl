using Bunzl.Domain.DTOs.Base;
using System.Text.Json.Serialization;

namespace Bunzl.Domain.Commands.Fornecedor.Atualizar;

public class FornecedorAtualizarResponse : BaseResponseDto
{
    public FornecedorAtualizarResponse(Guid id, string mensagem, Entities.Fornecedor fornecedor, bool atualizacaoSimples) : base(id, mensagem)
    {
        Fornecedor = fornecedor;
        AtualizacaoSimples = atualizacaoSimples;
    }

    public FornecedorAtualizarResponse(Guid id, string mensagem, Entities.Usuario usuario, Entities.Fornecedor fornecedor)
        : base(id, mensagem)
    {
        Usuario = usuario;
        Fornecedor = fornecedor;
    }

    public FornecedorAtualizarResponse(Guid id, string mensagem, bool emailParaFornecedor, List<Entities.Usuario> usuarios, Entities.Fornecedor fornecedor)
        : base(id, mensagem)
    {
        Usuarios = usuarios;
        EmailParaFornecedor = emailParaFornecedor;
        Fornecedor = fornecedor;
    }

    [JsonIgnore]
    public bool EmailParaFornecedor { get; set; }

    [JsonIgnore]
    public Entities.Fornecedor? Fornecedor { get; set; }

    [JsonIgnore]
    public Entities.Usuario? Usuario { get; set; }

    [JsonIgnore]
    public List<Entities.Usuario> Usuarios { get; set; }

    [JsonIgnore]
    public bool AtualizacaoSimples { get; set; }

}