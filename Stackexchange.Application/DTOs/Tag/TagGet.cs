namespace Stackexchange.Application.DTOs.Tag;

public class TagGet
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public int Count { get; set; }

    public bool IsRequired { get; set; }

    public bool IsModeratorOnly { get; set; }

    public bool HasSynonyms { get; set; }
}