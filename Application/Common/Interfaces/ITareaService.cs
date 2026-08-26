using Application.Dtos;
using Domain.Common;

namespace Application.Common.Interfaces
{
    public interface ITareaService
    {
        /// <summary>
        /// Método que sirve para obtener las tareas asociadas a un usuario
        /// </summary>
        /// <param name="id">Id del usuario</param>
        /// <param name="ct">Token de cancelación</param>
        /// <returns></returns>
        Task<Result<List<TareaDto>>> ListTareaByUsuarioId(int id, CancellationToken ct);

        /// <summary>
        /// Método que sirve para crear una tarea asociada a un usuario
        /// </summary>
        /// <param name="tareaDto">Objeto con la información de la tarea a crear</param>
        /// <param name="ct">Token de cancelación</param>
        /// <returns></returns>
        Task<Result<int>> CreateAsync(CrearTareaDto tareaDto, CancellationToken ct);
        
        /// <summary>
        /// Método que sirve para actualizar el estado de una tarea a completada o no completada
        /// </summary>
        /// <param name="tareaDto">Objeto con la información de la tarea a actualizar</param>
        /// <param name="ct">Token de cancelación</param>
        /// <returns></returns>
        Task<Result> UpdateAsync(ActualizarTareaDto tareaDto, CancellationToken ct);

        /// <summary>
        /// Método que sirve para eliminar una tarea
        /// </summary>
        /// <param name="id">Id de la tarea a eliminar</param>
        /// <param name="ct">Token de cancelación</param>
        /// <returns></returns>
        Task<Result> DeleteAsync(int id, CancellationToken ct);
    }
}
