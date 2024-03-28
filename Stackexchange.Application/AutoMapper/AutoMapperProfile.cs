using AutoMapper;
using Stackexchange.Application.DTOs.Tag;
using Stackexchange.Domain.Tags;

namespace Stackexchange.Application.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Tag, TagGet>();
    }
}