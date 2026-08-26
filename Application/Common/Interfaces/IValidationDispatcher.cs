namespace Application.Common.Interfaces
{
    public interface IValidationDispatcher
    {
        Task<List<string>> ValidateAsync<T>(T model) where T : class;
    }
}
