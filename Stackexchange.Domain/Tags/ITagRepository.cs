namespace Stackexchange.Domain.Tags;

public interface ITagRepository
{
    public Task<List<Tag>> GetTagsPageAsync(int page, int count, string sortField, bool asc);

    public Task ReplaceAllAsync(IEnumerable<Tag> tags);
}