using Bunzl.Infra.CrossCutting.Atributes;

namespace Bunzl.Domain.Enumerators;

public enum EPerfilUsuario
{
    [EnumDescriptionAttribute("Bunzl Corporativo (Master User)", "Bunzl Corporate (Master User)")] BunzlCorporativoMasterUser = 1,

    [EnumDescriptionAttribute("Administrador (Super User)", "Administrator (Super User)")] AdministradorSuperUser = 2,

    [EnumDescriptionAttribute("Comprador (Key User)", "Buyer (Key User)")] CompradorKeyUser = 3,

    [EnumDescriptionAttribute("Fornecedor (End User)", "Supplier (End User)")] FornecedorEndUser = 4
}