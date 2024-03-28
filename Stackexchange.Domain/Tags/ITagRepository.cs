namespace Stackexchange.Domain.Tags;

public interface ITagRepository
{
    public Task<List<Tag>> GetTagsAsync();
}