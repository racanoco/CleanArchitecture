using FluentValidation;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer
{
    public class UpdateStreamerCommandValidator : AbstractValidator<UpdateStreamerCommand>
    {
        public UpdateStreamerCommandValidator()
        {
            // Validaciones personalizadas de las propiedades de la entidad UpdateStreamerCommand
            RuleFor(x => x.Name)
                .NotNull().WithMessage("{Name} no permite valores nulos");

            RuleFor(x => x.Url)
                .NotNull().WithMessage("{Url} no permite valores nulos");
        }
    }
}
