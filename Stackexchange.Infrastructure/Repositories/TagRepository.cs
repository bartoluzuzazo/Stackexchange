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
}