using Application.Common.Interfaces;
using Application.Common.Repositories;
using Application.Dtos;
using Domain.Common;
using Domain.Entities;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IValidationDispatcher _validator;
        private readonly IUsuarioRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioService(IValidationDispatcher validator, IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _repository = usuarioRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Método para crear una usuario
        /// </summary>
        /// <param name="usuarioDto">Objeto con los datos del usuario</param>
        /// <param name="ct">Token de cancelación</param>
        /// <returns></returns>
        public async Task<Result<int>> CreateAsync(CrearUsuarioDto usuarioDto, CancellationToken ct)
        {
            var errores = await _validator.ValidateAsync(usuarioDto);

            if (errores.Count > 0)
                return Result<int>.Fail("Errores de validación.", 400, errores);

            if (await _repository.ExisteCorreoDeUsuario(0, usuarioDto.Email))
                return Result<int>.Fail("El correo electrónica ya existe", 400, errores);

            var resultUsuario = Usuario.Crear(usuarioDto.Nombre, usuarioDto.Email);

            if (!resultUsuario.Success)
                return Result<int>.Fail(resultUsuario.Message, resultUsuario.StatusCode, resultUsuario.Errors);

            var usuario = resultUsuario.Data!;

            await _repository.AddAsync(usuario, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return Result<int>.Ok(usuario.Id, 201);
        }

        /// <summary>
        /// Método que muestra la información de un usuario
        /// </summary>
        /// <param name="id">Id del usuario</param>
        /// <param name="ct">Token de cancelación</param>
        /// <returns></returns>
        public async Task<Result<UsuarioDto>> DetailsAsync(int id, CancellationToken ct)
        {
            var usuario = await _repository.GetByIdAsync(id, ct);

            if (usuario is null)
                return Result<UsuarioDto>.Fail("El usuario no existe.", 404);

            var usuarioDto = new UsuarioDto(usuario.Id, usuario.Nombre, usuario.Email);

            return Result<UsuarioDto>.Ok(usuarioDto, 200);
        }

        /// <summary>
        /// Método que sirve para actualizar la información de un usuario
        /// </summary>
        /// <param name="usuarioDto">Objeto con la información de un usuario</param>
        /// <param name="ct">Token de cancelación</param>
        /// <returns></returns>
        public async Task<Result> UpdateAsync(ActualizarUsuarioDto usuarioDto, CancellationToken ct)
        {
            var errores = await _validator.ValidateAsync(usuarioDto);

            if (errores.Count > 0)
                return Result.Fail("Errores de validación.", 400, errores);

            if (await _repository.ExisteCorreoDeUsuario(usuarioDto.Id, usuarioDto.Email))
                return Result.Fail("El correo electrónica ya existe", 400, errores);

            var usuario = await _repository.GetByIdAsync(usuarioDto.Id, ct);

            if (usuario is null)
                return Result.Fail("El usuario no existe.", 404);

            var resultUsuario = usuario.ActualizarUsuario(usuarioDto.Nombre, usuarioDto.Email);

            if (!resultUsuario.Success)
                return resultUsuario;

            _repository.Update(usuario);
            await _unitOfWork.SaveChangesAsync(ct);

            return Result.Ok(200);
        }

        /// <summary>
        /// Método que sirve para eliminar un usuario
        /// </summary>
        /// <param name="id">Id del usuario a eliminar</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<Result> DeleteAsync(int id, CancellationToken ct)
        {
            var usuario = await _repository.GetByIdAsync(id, ct);

            if (usuario is null)
                return Result.Fail("El usuario no existe.", 404);

            _repository.Delete(usuario);
            await _unitOfWork.SaveChangesAsync(ct);

            return Result.Ok(200);
        }
    }
}