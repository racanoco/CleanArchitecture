using CleanArchitecture.Application.Features.Videos.Queries;
using CleanArchitecture.Application.Features.Videos.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Net;

namespace CleanArchitecture.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VideoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region GET
        [HttpGet("{userName}", Name = "GetVideo")]
        [ProducesResponseType(typeof(IEnumerable<VideoViewModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable>> GetVideosByUserName(string userName)
        {
            var query = new GetVideosListQuery(userName);
            var videos = await _mediator.Send(query);
            return Ok(videos);
        }
        #endregion



    }
}
