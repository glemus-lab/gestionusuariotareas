using Application.Dtos;
using FluentValidation;

namespace Application.Validators
{
    public class CrearTareaValidator : AbstractValidator<CrearTareaDto>
    {
        public CrearTareaValidator()
        {
            RuleFor(s => s.Titulo)
                .NotEmpty().WithMessage("El titulo es requerido.")
                .MaximumLength(50).WithMessage("El título no puede contener más de 50 caracteres.");

            RuleFor(s => s.Descripcion)
                .MaximumLength(150).WithMessage("La descripción no puede contener más de 150 caracteres.")
                .Unless(s => string.IsNullOrWhiteSpace(s.Descripcion));
        }
    }
}
