using CleanArchitecture.Application.Features.Streamers.Commands;
using FluentValidation;

namespace CleanArchitecture.Application.Features.Streamers.Validators
{
    public class StreamerCommandValidator : AbstractValidator<StreamerCommand>
    {
        public StreamerCommandValidator()
        {
            // Reglas de validación para las propiedades
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{Name} no puede estar en blanco")
                .NotNull()
                .MaximumLength(50).WithMessage("{Name} no puede exceder los 50 caracteres");

            RuleFor(p => p.Url)
                .NotEmpty().WithMessage("{Url} no puede estar en blanco");                
                

        }
    }
}
