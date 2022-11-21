using FluentValidation;

namespace CleanArchitecture.Application.Features.Directors.Commands.CreaterDirector
{
    public class CreateDirectorCommandValidator : AbstractValidator<CreateDirectorCommand>
    {
        public CreateDirectorCommandValidator()
        {
            RuleFor( p => p.Name ).NotNull()
                .WithMessage("{Nombre} no puede ser nulo");

            RuleFor(p => p.LastName).NotNull()
                .WithMessage("{Nombre} no puede ser nulo");
        }
    }
}
