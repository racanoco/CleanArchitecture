﻿using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Features.Videos.Handler;
using CleanArchitecture.Application.Features.Videos.Queries;
using CleanArchitecture.Application.Features.Videos.ViewModels;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using Xunit;

namespace CleanArchitecture.Application.UnitTests.Features.Video.Queries
{
    public class GetVideosListQueryHandlerXUnitTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public GetVideosListQueryHandlerXUnitTests()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<VideoProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetVideosListTest()
        {
            string userName = "racanoco";
            var handler = new GetVideosListQueryHandler(_unitOfWork.Object, _mapper);
            var request = new GetVideosListQuery(userName);

            var result = await handler.Handle(request, CancellationToken.None);

            result.ShouldBeOfType<List<VideoViewModel>>();

            result.Count.ShouldBe(1);
        }
    }
}
