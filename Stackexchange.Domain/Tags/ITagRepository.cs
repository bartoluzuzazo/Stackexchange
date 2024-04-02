namespace Stackexchange.Domain.Tags;

public interface ITagRepository
{
    public Task<List<Tag>> GetTagsAsync();

    public Task<List<Tag>> GetTagsPageAsync(int page, int count, string sortField, bool asc);

    public Task ReplaceAllAsync(IEnumerable<Tag> tags);

    public Task FillAsync(IEnumerable<Tag> tags);

    public Task<int> GetTagCountAsync();

    public Task<int> GetPopulationCount();

    public Task<decimal> GetPopulationPercentage(Tag tag);
}