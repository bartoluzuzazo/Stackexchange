using AutoMapper;
using MediatR;
using Stackexchange.Application.DTOs.Tag;
using Stackexchange.Domain.Tags;

namespace Stackexchange.Application.TagServices.Queries;

public record GetTagQuery : IRequest<List<TagGet>>
{
    
}

public class GetTagQueryHandler : IRequestHandler<GetTagQuery, List<TagGet>>
{
    private readonly ITagRepository _tagRepository;
    private readonly IMapper _mapper;
    
    public GetTagQueryHandler(ITagRepository tagRepository, IMapper mapper)
    {
        _tagRepository = tagRepository;
        _mapper = mapper;
    }

    public async Task<List<TagGet>> Handle(GetTagQuery request, CancellationToken cancellationToken)
    {
        var result = await _tagRepository.GetTagsAsync();
        var response = result.Select(t => _mapper.Map<TagGet>(t)).ToList();
        return response;
    }
}