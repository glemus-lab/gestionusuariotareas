using Application.Dtos;
using FluentValidation;

namespace Application.Validators
{
    public class ActualizarTareaValidator : AbstractValidator<ActualizarTareaDto>
    {
        public ActualizarTareaValidator()
        {
            RuleFor(s => s.Id)
                .GreaterThan(0).WithMessage("El Id es requerido");
        }
    }
}
