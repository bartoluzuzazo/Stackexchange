namespace Stackexchange.Application.DTOs.SoTagWrapper;

public class SoTagDto
{
    public string name { get; set; } = null!;

    public int count { get; set; }

    public bool is_required { get; set; }

    public bool is_moderator_only { get; set; }

    public bool has_synonyms { get; set; }
}