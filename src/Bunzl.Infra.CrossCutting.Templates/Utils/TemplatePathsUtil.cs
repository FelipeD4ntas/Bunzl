namespace Bunzl.Infra.CrossCutting.Templates.Utils;

public static class TemplatePathsUtil
{
    public static string ObterPathEmail(string fileName) => $"Views/Emails/{fileName}.cshtml";
}
