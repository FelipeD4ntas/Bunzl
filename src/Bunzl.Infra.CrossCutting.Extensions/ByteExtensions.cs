namespace Bunzl.Infra.CrossCutting.Extensions;

public static class ByteExtensions
{
    public static byte TryParseOrDefault(this string value, byte defaultValue = 0)
    {
        return byte.TryParse(value, out var result) ? result : defaultValue;
    }
}