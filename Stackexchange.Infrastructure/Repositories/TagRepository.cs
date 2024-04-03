using Microsoft.EntityFrameworkCore;
using Stackexchange.Domain.Tags;
using Stackexchange.Infrastructure.Context;

namespace Stackexchange.Infrastructure.Repositories;

public class TagRepository(SeDbContext dbContext) : ITagRepository
{
    public async Task<List<Tag>> GetTagsPageAsync(int page, int count, string sortField, bool asc)
    {
        IQueryable<Tag> query = dbContext.Tags;
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
        dbContext.Tags.RemoveRange(dbContext.Tags);
        await dbContext.Tags.AddRangeAsync(tags);
        await dbContext.SaveChangesAsync();
    }
}