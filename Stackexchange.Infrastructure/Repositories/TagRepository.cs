using Microsoft.EntityFrameworkCore;
using Stackexchange.Domain.Tags;
using Stackexchange.Infrastructure.Context;

namespace Stackexchange.Infrastructure.Repositories;

public class TagRepository : ITagRepository
{
    private readonly SeDbContext _dbContext;

    public TagRepository(SeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Tag>> GetTagsAsync()
    {
        var result = await _dbContext.Tags.ToListAsync();
        return result;
    }

    public async Task<List<Tag>> GetTagsPageAsync(int page, int count, string sortField, bool asc)
    {
        IQueryable<Tag> query = _dbContext.Tags;
        if (sortField == "name")
        {
            query = asc ? query.OrderBy(t => t.Name) : query.OrderByDescending(t => t.Name);
        }
        else if (sortField == "percentage")
        {
            query = asc ? query.OrderBy(t => t.Percentage) : query.OrderByDescending(t => t.Percentage);
        }
        var result = await query.Skip((page - 1) * count).Take(count).ToListAsync();
        return result;
    }

    public async Task ReplaceAllAsync(IEnumerable<Tag> tags)
    {
        _dbContext.Tags.RemoveRange(_dbContext.Tags);
        await _dbContext.Tags.AddRangeAsync(tags);
        await _dbContext.SaveChangesAsync();
    }

    public async Task FillAsync(IEnumerable<Tag> tags)
    {
        await _dbContext.Tags.AddRangeAsync(tags);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<int> GetTagCountAsync()
    {
        var count = await _dbContext.Tags.CountAsync();
        return count;
    }

    public async Task<int> GetPopulationCount()
    {
        var count = await _dbContext.Tags.SumAsync(t => t.Count);
        return count;
    }

    public async Task<decimal> GetPopulationPercentage(Tag tag)
    {
        var count = await _dbContext.Tags.SumAsync(t => t.Count);
        return tag.Count/(decimal)count;
    }
}