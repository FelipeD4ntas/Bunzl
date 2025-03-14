using Bunzl.Infra.CrossCutting.Atributes;
using System.ComponentModel;

namespace Bunzl.Domain.Enumerators;

public enum EStatusTabelaPrecoProduto
{
    [EnumDescriptionAttribute("Aguardando Aprovação", "Waiting for Approval")] AguardandoAprovacao = 1,

    [EnumDescriptionAttribute("Aprovada", "Approved")] Aprovada = 2,

    [EnumDescriptionAttribute("Reprovada", "Rejected")] Reprovada = 3,

    [EnumDescriptionAttribute("Cancelada", "Cancelled")] Cancelada = 4
}
