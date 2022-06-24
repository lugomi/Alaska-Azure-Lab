namespace LabService.Models;

public record DatabaseConfiguration
{
    public string? ConnectionString { get; init; }
}