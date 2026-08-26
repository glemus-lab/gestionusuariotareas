using Application.Common.Repositories;
using Application.Dtos;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class TareaRepository : ITareaRepository
    {
        private readonly DbSet<Tarea> _dbSet;

        public TareaRepository(AppDbContext context)
        {
            _dbSet = context.Set<Tarea>();
        }

        /// <summary>
        /// Obtiene un listado de tareas que pertenecen a un usuario
        /// </summary>
        /// <param name="id">Id del usuario</param>
        /// <param name="ct">Token de cancelación</param>
        /// <returns>Listado de <see cref="Tarea"/></returns>
        public async Task<IEnumerable<Tarea>> ListTareaByUsuarioId(int id, CancellationToken ct)
            => await _dbSet.Where(s => s.UsuarioId == id).ToListAsync();


        /// <summary>
        /// Obtiene una tarea por su id
        /// </summary>
        /// <param name="id">Id de la tarea</param>
        /// <param name="ct">Token de cancelación</param>
        /// <returns><see cref="Tarea"/> que se quiere obtener</returns>
        public async Task<Tarea?> GetByIdAsync(int id, CancellationToken ct = default)
            => await _dbSet.FirstOrDefaultAsync(u => u.Id == id);

        /// <summary>
        /// Actualiza una <see cref="Tarea"/>
        /// </summary>
        /// <param name="tarea"><see cref="Tarea"/> a actualizar</param>
        public void Update(Tarea tarea)
            => _dbSet.Update(tarea);

        /// <summary>
        /// Eliminar una <see cref="Tarea"/>
        /// </summary>
        /// <param name="tarea"><see cref="Tarea"/> a eliminar</param>
        public void Delete(Tarea tarea)
            => _dbSet.Remove(tarea);
    }
}