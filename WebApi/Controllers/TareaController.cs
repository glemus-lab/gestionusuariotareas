using Application.Common.Interfaces;
using Application.Dtos;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace GestionUsuarioTareas.Controllers
{
    /// <summary>
    /// Controlador para la entidad Tarea
    /// </summary>
    [ApiController]
    [Route("api/tareas")]
    public class TareaController : ApiBaseController
    {
        private readonly ITareaService _tareaService;

        /// <summary>
        /// Constructor del controller
        /// </summary>
        /// <param name="tareaService"></param>
        public TareaController(ITareaService tareaService)
        {
            _tareaService = tareaService;
        }

        /// <summary>
        /// Muestra el listado de tareas de un usuario
        /// </summary>
        /// <param name="id">Id del usuario</param>
        /// <param name="ct">Token de cancelación</param>
        /// <returns>Retorna un objeto Result con un listado de tareas</returns>
        /// <response code="200">Retrona Result con el listado de tareas del usuario</response>
        [ProducesResponseType(typeof(Result<List<TareaDto>>), 200)]
        [HttpGet("usuario/{id}")]
        public async Task<IActionResult> Details(int id, CancellationToken ct = default)
        {
            var resultado = await _tareaService.ListTareaByUsuarioId(id, ct);
            return HandleResult(resultado);
        }

        /// <summary>
        /// Crear una tarea asociada a un usuario
        /// </summary>
        /// <param name="dto">Objeto <see cref="CrearTareaDto"/> que contiene el titulo, descripción y id del usuario</param>
        /// <param name="ct">Token de cancelación</param>
        /// <returns>Retorna un objeto Result con el id de la tarea recien creada</returns>
        /// <response code="201">Retorna el id de la tarea creada exitosamente</response>
        /// <response code="400">Si los datos no son validos</response>
        /// <response code="404">Si el id del usuario no existe</response>
        [ProducesResponseType(typeof(Result<int>), 201)]
        [ProducesResponseType(typeof(Result<int>), 400)]
        [ProducesResponseType(typeof(Result<int>), 404)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CrearTareaDto dto, CancellationToken ct = default)
        {
            var resultado = await _tareaService.CreateAsync(dto, ct);
            return HandleResult(resultado);
        }

        /// <summary>
        /// Cambio el estado de completado de una tarea
        /// </summary>
        /// <param name="id">Id de la tarea a actualizar</param>
        /// <param name="dto">Objeto <see cref="ActualizarTareaDto"/> con el id de la tarea y el estado de completada</param>
        /// <param name="ct">Token de cancelación</param>
        /// <returns>Retorna un objeto Result para indicar el estado de la transacción</returns>
        /// <response code="200">Si la operación fue exitosa</response>
        /// <response code="400">Si los datos no son validos</response>
        /// <response code="404">Si la tarea no existe</response>
        [ProducesResponseType(typeof(Result), 200)]
        [ProducesResponseType(typeof(Result), 400)]
        [ProducesResponseType(typeof(Result), 404)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ActualizarTareaDto dto, CancellationToken ct = default)
        {
            if (id != dto.Id)
                return HandleResult(Result.Fail("El id de la url no corresponde al id de cuerpo.", 400));

            var resultado = await _tareaService.UpdateAsync(dto, ct);
            return HandleResult(resultado);
        }

        /// <summary>
        /// Elimina un tarea
        /// </summary>
        /// <param name="id">Id de la tarea a eliminar</param>
        /// <param name="ct">Token de cancelación</param>
        /// <returns>Retorna un objeto Result para indicar el estado de la transacción</returns>
        /// <response code="200">Si la operación fue exitosa</response>
        /// <response code="404">Si la la tarea no existe</response>
        [ProducesResponseType(typeof(Result), 200)]
        [ProducesResponseType(typeof(Result), 404)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct = default)
        {
            var resultado = await _tareaService.DeleteAsync(id, ct);
            return HandleResult(resultado);
        }
    }
}
