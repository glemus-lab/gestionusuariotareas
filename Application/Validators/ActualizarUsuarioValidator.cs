using Application.Dtos;
using FluentValidation;

namespace Application.Validators
{
    public class ActualizarUsuarioValidator : AbstractValidator<ActualizarUsuarioDto>
    {
        public ActualizarUsuarioValidator()
        {
            RuleFor(s => s.Id)
                .GreaterThan(0).WithMessage("El Id es requerido");

            RuleFor(s => s.Nombre)
                .NotEmpty().WithMessage("El nombre es requerido")
                .MaximumLength(50).WithMessage("El nombre no puede contener más de 50 caracteres.");

            RuleFor(s => s.Email)
                .NotEmpty().WithMessage("El email es requerdio.")
                .MaximumLength(150).WithMessage("El email no puede contener más de 150 caracteres.")
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$").WithMessage("Formato de emial incorrecto.");
        }
    }
}
