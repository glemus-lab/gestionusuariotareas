using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Common
{
    public class ValidationDispatcher : IValidationDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidationDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<List<string>> ValidateAsync<T>(T model) where T : class
        {
            if (model is null)
                return ["La solicitud no puede estar vacía."];

            var validator = _serviceProvider.GetService<IValidator<T>>();

            if (validator is null)
            {
                return [];
            }

            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                return [.. validationResult.Errors.Select(e => e.ErrorMessage)];
            }

            return [];
        }
    }
}