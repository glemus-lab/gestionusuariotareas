using System.Linq.Expressions;
using Application.Common.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DbSet<Usuario> _dbSet;

        public UsuarioRepository(AppDbContext context)
        {
            _dbSet = context.Set<Usuario>();
        }

        /// <summary>
        /// Obtiene un Usuario por su Id
        /// </summary>
        /// <param name="id">Id del usuario</param>
        /// <param name="ct">Token de cancelación</param>
        /// <param name="includes">Parametros a incluir en el objeto <see cref="Usuario"/></param>
        /// <returns></returns>
        public async Task<Usuario?> GetByIdAsync(int id, CancellationToken ct = default, params Expression<Func<Usuario, object>>[] includes)
        {
            IQueryable<Usuario> query = _dbSet;
            foreach (var include in includes)
                query = query.Include(include);

            return await query.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Usuario?> GetByIdWithTareaAsync(int id, CancellationToken ct = default)
            => await _dbSet.Include(s => s.Tareas).FirstOrDefaultAsync(s => s.Id == id, ct);

        /// <summary>
        /// Agrega un usuario
        /// </summary>
        /// <param name="usuario">Usuario a agregar</param>
        /// <param name="ct">Token de cancelación</param>
        /// <returns></returns>
        public async Task AddAsync(Usuario usuario, CancellationToken ct = default)
            => await _dbSet.AddAsync(usuario, ct);

        /// <summary>
        /// Actualiza un usuario
        /// </summary>
        /// <param name="usuario"><see cref="Usuario"/> a actualizar</param>
        public void Update(Usuario usuario)
            => _dbSet.Update(usuario);

        /// <summary>
        /// Elimina un usuario
        /// </summary>
        /// <param name="usuario"><see cref="Usuario"/> a eliminar</param>
        public void Delete(Usuario usuario)
            => _dbSet.Remove(usuario);
    }
}
