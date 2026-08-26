using Infrastructure.Persistence;
using Application.Common.Interfaces;
using Domain.Entities;

namespace Infrastructure.Seed
{
    public static class DbInitializer
    {
        public static async Task Seed(AppDbContext context, IUnitOfWork unitOfWork)
        {
            context.Database.EnsureCreated();

            if (context.Usuarios.Any()) return;

            var usuarios = new List<Usuario>
            {
                Usuario.Crear("Juan Perez", "jperez@sistema.com").Data!,
                Usuario.Crear("Maria Chavez", "mchavez@sistema.com").Data!,
            };

            foreach (var usuario in usuarios)
            {
                for (int i = 1; i <= 5; i++)
                {
                    usuario.AgregarTarea(Tarea.Crear($"Tarea {i}", $"Realizar tarea {i}").Data!);
                }

            }

            await context.Usuarios.AddRangeAsync(usuarios);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
