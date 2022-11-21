﻿using MediatR;

namespace CleanArchitecture.Application.Features.Directors.Commands.CreaterDirector
{
    public class CreateDirectorCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public int VideoId { get; set; }
    }
}
