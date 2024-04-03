using AutoMapper;
using MediatR;
using Stackexchange.Application.DTOs.Tag;
using Stackexchange.Domain.Tags;

namespace Stackexchange.Application.TagServices.Queries;

public record GetTagPageQuery(int Page, int Count, string SortField, bool asc) : IRequest<List<TagGet>>;

public class GetTagPageQueryHandler(ITagRepository tagRepository, IMapper mapper) : IRequestHandler<GetTagPageQuery, List<TagGet>>
{
    public async Task<List<TagGet>> Handle(GetTagPageQuery request, CancellationToken cancellationToken)
    {
        var result = await tagRepository.GetTagsPageAsync(request.Page, request.Count, request.SortField, request.asc);
        var response = result.Select(mapper.Map<TagGet>).ToList();
        return response;
    }
}