using AutoMapper;
using CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Mappings
{
    public class StreamerProfile : Profile
    {
        public StreamerProfile()
        {
            CreateMap<CreateStreamerCommand, Streamer>()
                .ForMember(d => d.Name, o => o.MapFrom(c => c.Name))
                .ForMember(d => d.Url, o => o.MapFrom(c => c.Url));
        }
    }
}
