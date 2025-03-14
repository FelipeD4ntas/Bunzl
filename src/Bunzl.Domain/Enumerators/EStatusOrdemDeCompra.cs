using Bunzl.Infra.CrossCutting.Atributes;

namespace Bunzl.Domain.Enumerators;

public enum EStatusOrdemDeCompra
{
    [EnumDescriptionAttribute("Aguardando PI", "Waiting for PI")] AguardandoPi = 1,

    [EnumDescriptionAttribute("Aguardando Assinatura", "Waiting for Signature")] AguardandoAssinatura = 2,

    [EnumDescriptionAttribute("Cancelada", "Cancelled")] Cancelada = 3,

    [EnumDescriptionAttribute("Em Produção", "In Production")] EmProducao = 4,

    [EnumDescriptionAttribute("Finalizada", "Finalized")] Finalizada = 5
}
