using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Features.Videos.Queries;
using CleanArchitecture.Application.Features.Videos.ViewModels;
using MediatR;

namespace CleanArchitecture.Application.Features.Videos.Handler
{
    public class GetVideosListQueryHandler : IRequestHandler<GetVideosListQuery, List<VideoViewModel>>
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IMapper _mapper;

        public async Task<List<VideoViewModel>> Handle(GetVideosListQuery request, CancellationToken cancellationToken)
        {
            var videoList = await _videoRepository.GetVideoByUserName(request.UserName);

            return _mapper.Map<List<VideoViewModel>>(videoList);
        }
    }
}
