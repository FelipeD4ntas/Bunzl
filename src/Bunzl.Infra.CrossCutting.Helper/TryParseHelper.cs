namespace Bunzl.Infra.CrossCutting.Helper
{
    public static class TryParseHelper
    {
        public static byte ByteTryParseOrDefault(string? value, byte defaultValue = 0)
        {
            return byte.TryParse(value, out var result) ? result : defaultValue;
        }
    }
}
