using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Common.Repositories
{
    public interface IUsuarioRepository
    {
        /// <summary>
        /// Obtiene un Usuario por su Id
        /// </summary>
        /// <param name="id">Id del usuario</param>
        /// <param name="ct">Token de cancelación</param>
        /// <param name="includes">Parametros a incluir en el objeto <see cref="Usuario"/></param>
        /// <returns></returns>
        Task<Usuario?> GetByIdAsync(int id, CancellationToken ct = default, params Expression<Func<Usuario, object>>[] includes);
        
        /// <summary>
        /// Agrega un usuario
        /// </summary>
        /// <param name="usuario">Usuario a agregar</param>
        /// <param name="ct">Token de cancelación</param>
        /// <returns></returns>
        Task AddAsync(Usuario usuario, CancellationToken ct = default);
        
        /// <summary>
        /// Actualiza un usuario
        /// </summary>
        /// <param name="usuario"><see cref="Usuario"/> a actualizar</param>
        void Update(Usuario usuario);
        
        /// <summary>
        /// Elimina un usuario
        /// </summary>
        /// <param name="usuario"><see cref="Usuario"/> a eliminar</param>
        void Delete(Usuario usuario);
    }
}