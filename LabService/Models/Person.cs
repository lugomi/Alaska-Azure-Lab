using MongoDB.Bson.Serialization.Attributes;

namespace LabService.Models;

[BsonIgnoreExtraElements]
public record Person
{
    public string Name { get; init; } = string.Empty;
    public int Age { get; init; }
    public string FavoriteColor { get; init; } = string.Empty;
}