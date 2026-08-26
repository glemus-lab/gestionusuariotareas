using Application.Common.Interfaces;
using Application.Dtos;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace GestionUsuarioTareas.Controllers
{
    /// <summary>
    /// Controlado para la entidad Usuario
    /// </summary>
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ApiBaseController
    {
        private readonly IUsuarioService _usuarioService;

        /// <summary>
        /// Constructor del controller de Usuario
        /// </summary>
        /// <param name="usuarioService"></param>
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Obtiene la información de un usuario
        /// </summary>
        /// <param name="id">Id del usuario</param>
        /// <param name="ct">Token de cancelación</param>
        /// <returns></returns>
        /// <response code="200">Retorna un objeto result con un objeto que contiene el id, nombre y correo del usuario</response>
        /// <response code="404">Retorna un objeto result cuando el usuario no existe</response>
        [ProducesResponseType(typeof(Result<UsuarioDto>), 200)]
        [ProducesResponseType(typeof(Result<UsuarioDto>), 404)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id, CancellationToken ct = default)
        {
            var resultado = await _usuarioService.DetailsAsync(id, ct);
            return HandleResult(resultado);
        }

        /// <summary>
        /// Creacion de un usuario
        /// </summary>
        /// <param name="dto">Objeto con el nombre y correco electronico del usuario</param>
        /// <param name="ct">Token de cancelación</param>
        /// <returns>Retorna un objeto Result con el Id del usuario recien creado</returns>
        /// <response code="201">Retorna el id del usuario creado exitosamente</response>
        /// <response code="400">Si los datos no son validos</response>
        [ProducesResponseType(typeof(Result<int>), 201)]
        [ProducesResponseType(typeof(Result<int>), 400)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CrearUsuarioDto dto, CancellationToken ct = default)
        {
            var resultado = await _usuarioService.CreateAsync(dto, ct);
            return HandleResult(resultado);
        }

        /// <summary>
        /// Actualiza la información de un usuario
        /// </summary>
        /// <param name="id">Id del usuario</param>
        /// <param name="dto">Objeto con el Id, nombre y correo del usuario</param>
        /// <param name="ct">Token de cancelación</param>
        /// <returns>Retorna un objeto result para verificar el estado de la transacción</returns>
        /// <response code="200">Retorna el objeto Result cuando la operación fue exitosa</response>
        /// <response code="400">Retorna el objeto Result con los mensajes de error</response>
        /// <response code="404">Retorna el objeto Result cuando el usuario no existe</response>
        [ProducesResponseType(typeof(Result), 200)]
        [ProducesResponseType(typeof(Result), 400)]
        [ProducesResponseType(typeof(Result), 404)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ActualizarUsuarioDto dto, CancellationToken ct = default)
        {
            if (id != dto.Id)
                return HandleResult(Result.Fail("El id de la url no corresponde al id de cuerpo.", 400));

            var resultado = await _usuarioService.UpdateAsync(dto, ct);
            return HandleResult(resultado);
        }

        /// <summary>
        /// Elimina un usuario
        /// </summary>
        /// <param name="id">Id del usuario a eliminar</param>
        /// <param name="ct">Token de cancelación</param>
        /// <returns>Retorna un objeto result para verificar el estado de la transacción</returns>
        /// <response code="200">Retorna el objeto Result cuando la operación fue exitosa</response>
        /// <response code="404">Retorna el objeto Result cuando el usuario no existe</response>
        [ProducesResponseType(typeof(Result), 200)]
        [ProducesResponseType(typeof(Result), 404)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct = default)
        {
            var resultado = await _usuarioService.DeleteAsync(id, ct);
            return HandleResult(resultado);
        }
    }
}