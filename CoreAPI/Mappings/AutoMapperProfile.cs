using AutoMapper;
using YukiAPI.Dtos;
using Core.Entities;

namespace YukiAPI.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Post, PostDto>()
            .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Content.Length > 100 ? src.Content.Substring(0, 100) : src.Content))
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));
    }
}