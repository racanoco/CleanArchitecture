using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Features.Videos.Queries;
using CleanArchitecture.Application.Features.Videos.ViewModels;
using MediatR;

namespace CleanArchitecture.Application.Features.Videos.Handler
{
    public class GetVideosListQueryHandler : IRequestHandler<GetVideosListQuery, List<VideoViewModel>>
    {
        // private readonly IVideoRepository _videoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetVideosListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            // _videoRepository = videoRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<VideoViewModel>> Handle(GetVideosListQuery request, CancellationToken cancellationToken)
        {
            // var videoList = await _videoRepository.GetVideoByUserName(request.UserName);
            var videoList = await _unitOfWork.VideoRepository.GetVideoByUserName(request.UserName);

            return _mapper.Map<List<VideoViewModel>>(videoList);
        }
    }
}
