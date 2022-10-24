using CleanArchitecture.Application.Features.Videos.ViewModels;
using MediatR;

namespace CleanArchitecture.Application.Features.Videos.Queries
{
    public class GetVideosListQuery : IRequest<List<VideoViewModel>>
    {
        public string UserName { get; set; } = string.Empty;

        public GetVideosListQuery(string userName)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));   
        }
    }
}
