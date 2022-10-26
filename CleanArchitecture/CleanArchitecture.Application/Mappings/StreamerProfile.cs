using AutoMapper;
using CleanArchitecture.Application.Features.Streamers.Commands;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Mappings
{
    public class StreamerProfile : Profile
    {
        public StreamerProfile()
        {
            CreateMap<StreamerCommand, Streamer>()
                .ForMember(d => d.Name, o => o.MapFrom(c => c.Name))
                .ForMember(d => d.Url, o => o.MapFrom(c => c.Url)); ;
        }
    }
}
