using Bunzl.Infra.CrossCutting.Atributes;

namespace Bunzl.Domain.Enumerators;

public enum EStatusFornecedor
{
    [EnumDescriptionAttribute("Aguardando Aprovação", "Waiting for Approval")] AguardandoAprovacao = 1,

    [EnumDescriptionAttribute("Homologado", "Approved")] Homologado = 2,

    [EnumDescriptionAttribute("Não Homologado", "Not Approved")] NaoHomologado = 3,

    [EnumDescriptionAttribute("Inativo", "Inactive")] Inativo = 4,

    [EnumDescriptionAttribute("Inválido para uso no Portal", "Invalid for use on the Portal")] InvalidoPortal = 5
}