namespace Bunzl.Infra.CrossCutting.Atributes;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class EnumDescriptionAttribute(string descriptionPtBr, string descriptionEn) : Attribute
{
    public string DescriptionPtBr { get; } = descriptionPtBr;
    public string DescriptionEn { get; } = descriptionEn;
}