using Bunzl.Infra.CrossCutting.Atributes;

namespace Bunzl.Domain.Enumerators;

public enum EStatusTabelaPreco
{
    [EnumDescriptionAttribute("Aguardando Aprovação", "Waiting for Approval")] AguardandoAprovacao = 1,

    [EnumDescriptionAttribute("Validada", "Validated")] Validada = 2,

    [EnumDescriptionAttribute("Cancelada", "Cancelled")] Cancelada = 3,
    
    [EnumDescriptionAttribute("Integrada ao Portal e ao ERP", "Integrated to the Portal and ERP")] Integrada = 4,

    [EnumDescriptionAttribute("Reprovada", "Rejected")] Reprovada = 5
}
