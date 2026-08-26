using Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace GestionUsuarioTareas.Controllers
{
    /// <summary>
    /// Base para los controller del API
    /// </summary>
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        /// <summary>
        /// Método para resolver un objeto <see cref="Result"/>
        /// </summary>
        /// <param name="result">Objeto de retorno</param>
        /// <returns>Retorna el objeto Result</returns>
        protected ActionResult HandleResult(Result result)
        {
            if (result.Success)
            {
                return result.StatusCode switch
                {
                    204 => NoContent(),
                    _ => Ok(result)
                };
            }

            return ProcesarError(result);
        }

        /// <summary>
        /// Método para resolver un <see cref="Result{T}"/>
        /// </summary>
        /// <typeparam name="T">Tipo del la data que retorna</typeparam>
        /// <param name="result">Objeto result con la data</param>
        /// <returns>Retorna un objeto Result del tipo T</returns>
        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result.Success)
            {
                return result.StatusCode switch
                {
                    201 => Created("", result),
                    _ => Ok(result)
                };
            }

            return ProcesarError(result);
        }

        private ActionResult ProcesarError(Result result) => result.StatusCode switch
        {
            400 => BadRequest(result),
            404 => NotFound(result),
            _ => StatusCode(result.StatusCode, result)
        };
    }
}