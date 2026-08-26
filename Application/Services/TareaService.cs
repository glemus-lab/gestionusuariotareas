using Application.Common.Interfaces;
using Application.Common.Repositories;
using Application.Dtos;
using Domain.Common;
using Domain.Entities;

namespace Application.Services
{
    public class TareaService : ITareaService
    {
        private readonly IValidationDispatcher _validator;
        private readonly ITareaRepository _repository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TareaService(IValidationDispatcher validator, ITareaRepository repository, IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _repository = repository;
            _usuarioRepository = usuarioRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Método que sirve para obtener las tareas asociadas a un usuario
        /// </summary>
        /// <param name="id">Id del usuario</param>
        /// <param name="ct">Token de cancelación</param>
        /// <returns></returns>
        public async Task<Result<List<TareaDto>>> ListTareaByUsuarioId(int id, CancellationToken ct)
        {
            var listadoTarea = await _repository.ListTareaByUsuarioId(id, ct);

            var listadoDto = listadoTarea.Select(s => new TareaDto(s.Id, s.Titulo, s.Descripcion, s.Completada)).ToList();

            return Result<List<TareaDto>>.Ok(listadoDto, 200);
        }

        /// <summary>
        /// Método que sirve para crear una tarea asociada a un usuario
        /// </summary>
        /// <param name="tareaDto">Objeto con la información de la tarea a crear</param>
        /// <param name="ct">Token de cancelación</param>
        /// <returns></returns>
        public async Task<Result<int>> CreateAsync(CrearTareaDto tareaDto, CancellationToken ct)
        {
            var errores = await _validator.ValidateAsync(tareaDto);

            if (errores.Count > 0)
                return Result<int>.Fail("Errores de validación.", 400, errores);

            var usuario = await _usuarioRepository.GetByIdAsync(tareaDto.UsuarioId, ct);

            if (usuario is null)
                return Result<int>.Fail("El usuario no existe", 404);

            var resultTarea = Tarea.Crear(tareaDto.Titulo, tareaDto.Descripcion);

            if (!resultTarea.Success)
                return Result<int>.Fail(resultTarea.Message, resultTarea.StatusCode, resultTarea.Errors);

            var tarea = resultTarea.Data!;

            usuario.AgregarTarea(tarea);

            _usuarioRepository.Update(usuario);
            await _unitOfWork.SaveChangesAsync(ct);

            return Result<int>.Ok(tarea.Id, 201);
        }

        /// <summary>
        /// Método que sirve para eliminar una tarea
        /// </summary>
        /// <param name="id">Id de la tarea a eliminar</param>
        /// <param name="ct">Token de cancelación</param>
        /// <returns></returns>
        public async Task<Result> DeleteAsync(int id, CancellationToken ct)
        {
            var tarea = await _repository.GetByIdAsync(id, ct);

            if (tarea is null)
                return Result.Fail("La tarea no existe.", 404);

            _repository.Delete(tarea);
            await _unitOfWork.SaveChangesAsync(ct);

            return Result.Ok(200);
        }

        /// <summary>
        /// Método que sirve para actualizar el estado de una tarea a completada o no completada
        /// </summary>
        /// <param name="tareaDto">Objeto con la información de la tarea a actualizar</param>
        /// <param name="ct">Token de cancelación</param>
        /// <returns></returns>
        public async Task<Result> UpdateAsync(ActualizarTareaDto tareaDto, CancellationToken ct)
        {
            var errores = await _validator.ValidateAsync(tareaDto);

            if (errores.Count > 0)
                return Result.Fail("Errores de validación.", 400, errores);

            var tarea = await _repository.GetByIdAsync(tareaDto.Id, ct);

            if (tarea is null)
                return Result.Fail("La tarea no existe.", 404);

            tarea.CambiarEstadoCompletada(tareaDto.Completada);

            _repository.Update(tarea);
            await _unitOfWork.SaveChangesAsync(ct);

            return Result.Ok(200);
        }
    }
}