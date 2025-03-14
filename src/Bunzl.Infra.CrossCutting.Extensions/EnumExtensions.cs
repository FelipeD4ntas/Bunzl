using Bunzl.Core.Domain.Enumerators;
using prmToolkit.EnumExtension;
using System.ComponentModel;
using System.Reflection;
using Bunzl.Infra.CrossCutting.Atributes;

namespace Bunzl.Infra.CrossCutting.Extensions;

public static class EnumExtensaion
{
    public static List<EnumDto> ToDtoList<T>(this T enumTipo, string idioma, params T[] exluirOpcoes) where T : Enum
    {
        var enumVals = new List<EnumDto>();

        foreach (var item in Enum.GetValues(typeof(T)))
        {
            var enumValue = (T)item;

            // Verifica se o valor do enum está na lista de exclusão
            if (exluirOpcoes.Contains(enumValue))
                continue; // Pula para o próximo item se o valor estiver na lista de exclusão

            enumVals.Add(new EnumDto
            {
                Id = (int)item,
                Name = enumValue.GetName(),
                Descricao = enumValue.GetDescriptionLanguage(idioma)
            });
        }

        return enumVals.OrderBy(p => p.Descricao).ToList();
    }

    public static string GetDescriptionLanguage(this Enum value, string language = "pt-BR")
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field.GetCustomAttribute<EnumDescriptionAttribute>();

        if (attribute != null)
        {
            return language switch
            {
                "en" => attribute.DescriptionEn,
                "br" => attribute.DescriptionPtBr,
                _ => value.ToString(), // Retorna o nome do enum se o idioma não for reconhecido
            };
        }

        return value.ToString();
    }
}