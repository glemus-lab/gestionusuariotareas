using Domain.Common;

namespace Domain.Entities
{
    /// <summary>
    /// Entidad que representa la entidad de una Tarea
    /// </summary>
    public class Tarea
    {
        /// <summary>
        /// Id de la tarea
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Titulo de la tarea
        /// </summary>
        public string Titulo { get; private set; } = string.Empty;
        
        /// <summary>
        /// Descripción de la tarea
        /// </summary>
        public string? Descripcion { get; private set; }
        
        /// <summary>
        /// Estado de la tarea
        /// </summary>
        public bool Completada { get; private set; } = false;

        /// <summary>
        /// Id del usuario asociado
        /// </summary>
        public int UsuarioId { get; private set; }
        
        /// <summary>
        /// Propiedad de navegación del Usuario
        /// </summary>
        public virtual Usuario? Usuario { get; private set; }

        /// <summary>
        /// Contructor de la entidad Tarea
        /// </summary>
        /// <param name="titulo">Titulo de la tarea</param>
        /// <param name="descripcion">Descripción de la tarea</param>
        private Tarea(string titulo, string? descripcion)
        {
            Titulo = titulo;
            Descripcion = descripcion;
            Completada = false;
        }

        /// <summary>
        /// Método para crear una instancia valida de una Tarea
        /// </summary>
        /// <param name="titulo">Tuitulo de la tarea</param>
        /// <param name="descripcion">Descripción de la tarea</param>
        /// <returns>Objeto Result con la instancia de Tarea</returns>
        public static Result<Tarea> Crear(string titulo, string? descripcion)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(titulo))
                errors.Add("El titulo es requerido.");
            else if (titulo.Length > 50)
                errors.Add("El título no puede contener más de 50 caracteres.");

            if (!string.IsNullOrWhiteSpace(descripcion) && descripcion.Length > 150)
                errors.Add("La descripción no puede contener más de 150 caracteres.");

            if (errors.Count > 0)
                return Result<Tarea>.Fail("Errores de validación.", 400, errors);

            var tarea = new Tarea(titulo, descripcion);

            return Result<Tarea>.Ok(tarea, 200);
        }

        /// <summary>
        /// Método que sirve para cambiar el estado de una tarea
        /// </summary>
        /// <param name="completada">Valor booleano para indicar si la tarea se ha completado</param>
        public void CambiarEstadoCompletada(bool completada) => Completada = completada;
    }
}