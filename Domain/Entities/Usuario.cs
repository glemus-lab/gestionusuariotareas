using System.Text.RegularExpressions;
using Domain.Common;

namespace Domain.Entities
{
    /// <summary>
    /// Entidad que representa un Usuario
    /// </summary>
    public partial class Usuario
    {
        /// <summary>
        /// Id del usuario
        /// </summary>
        public int Id { get; private set; }
        
        /// <summary>
        /// Nombre del usuario
        /// </summary>
        public string Nombre { get; private set; } = string.Empty;

        /// <summary>
        /// Correo del usuario
        /// </summary>
        public string Email { get; private set; } = string.Empty;

        /// <summary>
        /// Listado de tareas asociadas al usuario
        /// </summary>
        private readonly List<Tarea> _tareas = [];
        
        /// <summary>
        /// Propiedad de navegación para las tareas
        /// </summary>
        public virtual IReadOnlyCollection<Tarea> Tareas => _tareas.AsReadOnly();

        /// <summary>
        /// Contructor de la entidad Usuario
        /// </summary>
        /// <param name="nombre">Nombre del usuario</param>
        /// <param name="email">Correo del usuario</param>
        private Usuario(string nombre, string email)
        {
            Nombre = nombre;
            Email = email;
        }
                
        /// <summary>
        /// Método para crear una instancia valida de un Usuario
        /// </summary>
        /// <param name="nombre">Nombre del usuario</param>
        /// <param name="email">Correo del usuario</param>
        /// <returns>Objeto Result con la instancia de Usuario</returns>
        public static Result<Usuario> Crear(string nombre, string email)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(nombre))
                errors.Add("El nombre es requerido.");
            else if (nombre.Length > 50)
                errors.Add("El nombre no puede contener más de 50 caracteres.");

            if (string.IsNullOrWhiteSpace(email))
                errors.Add("El email es requerdio.");
            else if (email.Length > 150)
                errors.Add("El email no puede contener más de 150 caracteres.");
            else if (!EmailRegex().IsMatch(email))
                errors.Add("Formato de emial incorrecto.");

            if (errors.Count > 0)
                return Result<Usuario>.Fail("Errores de validación.", 400, errors);

            var usuario = new Usuario(nombre, email);

            return Result<Usuario>.Ok(usuario, 200);
        }

        /// <summary>
        /// Método para agregar una tarea a un usuario
        /// </summary>
        /// <param name="tarea">Objeto de tipo <see cref="Tarea"/> que se asocia a un usuario</param>
        public void AgregarTarea(Tarea tarea)
        {
            _tareas.Add(tarea);
        }

        /// <summary>
        /// Método para actualizar la información de un usuario
        /// </summary>
        /// <param name="nombre">Nombre del usuario</param>
        /// <param name="email">Correo del usuario</param>
        /// <returns>Objeto Result para indicar el estado de la actualización</returns>
        public Result ActualizarUsuario(string nombre, string email)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(nombre))
                errors.Add("El nombre es requerido.");
            else if (nombre.Length > 50)
                errors.Add("El nombre no puede contener más de 50 caracteres.");

            if (string.IsNullOrWhiteSpace(email))
                errors.Add("El email es requerdio.");
            else if (email.Length > 150)
                errors.Add("El email no puede contener más de 150 caracteres.");
            else if (!EmailRegex().IsMatch(email))
                errors.Add("Formato de emial incorrecto.");

            if (errors.Count > 0)
                return Result.Fail("Errores de validación.", 400, errors);

            Nombre = nombre;
            Email= email;

            return Result.Ok(200);
        }

        [GeneratedRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
        private static partial Regex EmailRegex();
    }
}
