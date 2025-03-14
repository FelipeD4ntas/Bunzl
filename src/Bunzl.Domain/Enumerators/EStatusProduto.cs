using Bunzl.Infra.CrossCutting.Atributes;

namespace Bunzl.Domain.Enumerators;

public enum EStatusProduto
{
    [EnumDescriptionAttribute("Homologado", "Approved")] Homologado = 1,

    [EnumDescriptionAttribute("Não Homologado", "Not Approved")] NaoHomologado = 2,

    [EnumDescriptionAttribute("Suspenso", "Suspended")] Suspenso = 3
}
