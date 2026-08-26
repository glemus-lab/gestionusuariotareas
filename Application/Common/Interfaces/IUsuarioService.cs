using Application.Dtos;
using Domain.Common;

namespace Application.Common.Interfaces
{
    public interface IUsuarioService
    {
        /// <summary>
        /// Método para crear una usuario
        /// </summary>
        /// <param name="usuarioDto">Objeto con los datos del usuario</param>
        /// <param name="ct">Token de cancelación</param>
        /// <returns></returns>
        Task<Result<int>> CreateAsync(CrearUsuarioDto usuarioDto, CancellationToken ct);

        /// <summary>
        /// Método que muestra la información de un usuario
        /// </summary>
        /// <param name="id">Id del usuario</param>
        /// <param name="ct">Token de cancelación</param>
        /// <returns></returns>
        Task<Result<UsuarioDto>> DetailsAsync(int id, CancellationToken ct);

        /// <summary>
        /// Método que sirve para actualizar la información de un usuario
        /// </summary>
        /// <param name="usuarioDto">Objeto con la información de un usuario</param>
        /// <param name="ct">Token de cancelación</param>
        /// <returns></returns>
        Task<Result> UpdateAsync(ActualizarUsuarioDto usuarioDto, CancellationToken ct);

        /// <summary>
        /// Método que sirve para eliminar un usuario
        /// </summary>
        /// <param name="id">Id del usuario a eliminar</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<Result> DeleteAsync(int id, CancellationToken ct);
    }
}
