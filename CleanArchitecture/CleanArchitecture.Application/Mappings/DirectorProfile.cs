using AutoMapper;
using CleanArchitecture.Application.Features.Directors.Commands.CreaterDirector;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Mappings
{
    public class DirectorProfile : Profile
    {
        public DirectorProfile()
        {
            CreateMap<CreateDirectorCommand, Director>()
                .ForMember(d => d.Name, o => o.MapFrom(c => c.Name))
                .ForMember(d => d.LastName, o => o.MapFrom(c => c.LastName))
                .ForMember(d => d.VideoId, o => o.MapFrom(c => c.VideoId));
        }
    }
}
