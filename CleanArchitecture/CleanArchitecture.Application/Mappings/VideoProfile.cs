using AutoMapper;
using CleanArchitecture.Application.Features.Videos.ViewModels;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Mappings
{
    public class VideoProfile : Profile
    {
        public VideoProfile()
        {
            CreateMap<Video, VideoViewModel>()
                .ForMember(d => d.Name, o => o.MapFrom(c => c.Name))
                .ForMember(d => d.StreamerId, o => o.MapFrom(c => c.StreamerId));
        }
    }
}
