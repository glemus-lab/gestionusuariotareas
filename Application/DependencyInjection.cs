using System.Reflection;
using Application.Common;
using Application.Common.Interfaces;
using Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<IValidationDispatcher, ValidationDispatcher>();

        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<ITareaService, TareaService>();

        return services;
    }
}
