namespace Application.Dtos
{
    public record UsuarioWithTareasDto(int Id, string Nombre, string Email, List<TareaDto> Tareas);
}
