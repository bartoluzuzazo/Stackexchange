using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using MediatR;
using Stackexchange.Application.DTOs.SoTagWrapper;
using Stackexchange.Domain.Tags;

namespace Stackexchange.Application.TagServices.Commands;

public record PostTagCommand() : IRequest;

public class PostTagCommandHandler(ITagRepository tagRepository) : IRequestHandler<PostTagCommand>
{
    public async Task Handle(PostTagCommand request, CancellationToken cancellationToken)
    {
        var clientHandler = new HttpClientHandler()
            { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
        var client = new HttpClient(clientHandler);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var tags = new List<Tag>();

        var dbLimit = int.Parse(Environment.GetEnvironmentVariable("DB_TAG_LIMIT") ?? "1000");
        var iterations = dbLimit / 100;
        if (iterations < 1) iterations = 1;

        for (var page = 1; page <= iterations; page++)
        {
            using var result =
                await client.GetAsync(
                    $"https://api.stackexchange.com/2.3/tags?page={page}&pagesize=100&order=desc&sort=popular&site=stackoverflow");
            if (!result.IsSuccessStatusCode) throw new Exception(result.StatusCode.ToString());
            var str = await result.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<TagWrapper>(str);
            response?.items.ForEach(t =>
            {
                var tag = new Tag()
                {
                    Id = Guid.NewGuid(),
                    Name = t.name,
                    Count = t.count,
                    HasSynonyms = t.has_synonyms,
                    IsModeratorOnly = t.is_moderator_only,
                    IsRequired = t.is_required
                };
                tags.Add(tag);
            });
        }

        var totalCount = tags.Sum(t => t.Count);
        tags.ForEach(t => t.Percentage = t.Count / (decimal)totalCount * 100);
        await tagRepository.ReplaceAllAsync(tags);
    }
}