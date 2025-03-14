using Bunzl.Infra.CrossCutting.Atributes;

namespace Bunzl.Domain.Enumerators;

public enum EStatusNegociacaoComercial
{
    [EnumDescriptionAttribute("Em Negociação", "In Negotiation")] EmNegociacao = 1,

    [EnumDescriptionAttribute("Aceita", "Accepted")] Aceita = 2,

    [EnumDescriptionAttribute("Recusada", "Rejected")] Recusada = 3,

    [EnumDescriptionAttribute("Concluída", "Completed")] Concluida = 4
}

