using Bunzl.Domain.Enumerators;

namespace Bunzl.Domain.DTOs;

public class TabelaPrecoHistoricoDto
{
    public Guid Id { get; set; }
    public DateTime DataCriacao { get; set; }
    public EStatusTabelaPreco Status { get; set; }
}

