using Bunzl.Infra.CrossCutting.Atributes;

namespace Bunzl.Domain.Enumerators;

public enum ETipoDocumento
{
    [EnumDescriptionAttribute("Imagem do Produto", "Product Image")] ImagemProduto = 1,

    [EnumDescriptionAttribute("Relatórios de Testes", "Test Reports")] RelatoriosTeste = 2,

    [EnumDescriptionAttribute("Documentos", "Documents")] Documentos = 3,

    [EnumDescriptionAttribute("Ficha Técnica", "Technical Data Sheet")] FichaTecnica = 4
}
