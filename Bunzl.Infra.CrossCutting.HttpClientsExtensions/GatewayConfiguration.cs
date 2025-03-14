namespace Bunzl.Infra.CrossCutting.HttpClientsExtensions;

public class GatewayConfiguration
{
    public string? BaseUrl { get; set; }
    public string? Environment { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
}
