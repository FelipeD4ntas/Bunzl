namespace Bunzl.Infra.CrossCutting.ActiveDirectory.DTOs;

public class AdConfig
{
    public required string Domain { get; set; }
    public required string Container { get; set; }
    public required bool SkipValidation { get; set; }
}