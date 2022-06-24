namespace LabService.Models;

public record Error
{
    public string Message { get; init; } = string.Empty;
}