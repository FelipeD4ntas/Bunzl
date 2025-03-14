using Bunzl.Domain.Enumerators;

namespace Bunzl.Domain.Commands.Auditoria.Listar;

public class AuditoriaListarResponse(Guid id, Guid entidadeId, string entidadeNome, string funcao, Guid usuarioCriacao, string usuarioNome, DateTime dataCriacao, ETipoAuditoria tipoAuditoria)
{
    public Guid Id { get; set; } = id;
    public Guid EntidadeId { get; set; } = entidadeId;
    public string EntidadeNome { get; set; } = entidadeNome;
    public string Funcao { get; set; } = funcao;
    public Guid UsuarioCriacao { get; set; } = usuarioCriacao;
    public string UsuarioNome { get; set; } = usuarioNome;
    public DateTime DataCriacao { get; set; } = dataCriacao;
    public ETipoAuditoria TipoAuditoria { get; set; } = tipoAuditoria;
}
