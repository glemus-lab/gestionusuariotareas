using Domain.Common;
using Domain.Entities;

namespace Application.Common.Repositories
{
    public interface ITareaRepository
    {
        /// <summary>
        /// Obtiene un listado de tareas que pertenecen a un usuario
        /// </summary>
        /// <param name="id">Id del usuario</param>
        /// <param name="ct">Token de cancelación</param>
        /// <returns>Listado de <see cref="Tarea"/></returns>
        Task<IEnumerable<Tarea>> ListTareaByUsuarioId(int id, CancellationToken ct = default);
        
        /// <summary>
        /// Obtiene una tarea por su id
        /// </summary>
        /// <param name="id">Id de la tarea</param>
        /// <param name="ct">Token de cancelación</param>
        /// <returns><see cref="Tarea"/> que se quiere obtener</returns>
        Task<Tarea?> GetByIdAsync(int id, CancellationToken ct = default);
        
        /// <summary>
        /// Actualiza una <see cref="Tarea"/>
        /// </summary>
        /// <param name="tarea"><see cref="Tarea"/> a actualizar</param>
        void Update(Tarea tarea);
        
        /// <summary>
        /// Eliminar una <see cref="Tarea"/>
        /// </summary>
        /// <param name="tarea"><see cref="Tarea"/> a eliminar</param>
        void Delete(Tarea tarea);
    }
}
