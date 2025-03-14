namespace Bunzl.Domain.DTOs;

public class OrdemDeCompraObservacaoListarDto(Guid id, Guid usuarioCriacao, string usuarioNome, DateTime dataCriacao, string observacao)
{
    public Guid Id { get; set; } = id;
    public Guid UsuarioCriacao { get; set; } = usuarioCriacao;
    public string UsuarioNome { get; set; } = usuarioNome;
    public DateTime DataCriacao { get; set; } = dataCriacao;
    public string Observacao { get; set; } = observacao;
}

