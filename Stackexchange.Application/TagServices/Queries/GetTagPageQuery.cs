using AutoMapper;
using MediatR;
using Stackexchange.Application.DTOs.Tag;
using Stackexchange.Domain.Tags;

namespace Stackexchange.Application.TagServices.Queries;

public record GetTagPageQuery(int Page, int Count, string SortField, bool asc) : IRequest<List<TagGet>>;

public class GetTagPageQueryHandler : IRequestHandler<GetTagPageQuery, List<TagGet>>
{
    private readonly ITagRepository _tagRepository;
    private readonly IMapper _mapper;

    public GetTagPageQueryHandler(ITagRepository tagRepository, IMapper mapper)
    {
        _tagRepository = tagRepository;
        _mapper = mapper;
    }

    public async Task<List<TagGet>> Handle(GetTagPageQuery request, CancellationToken cancellationToken)
    {
        var result = await _tagRepository.GetTagsPageAsync(request.Page, request.Count, request.SortField, request.asc);
        var response = result.Select(t => _mapper.Map<TagGet>(t)).ToList();
        return response;
    }
}