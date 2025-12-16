namespace Harmonia.API.Services;

public interface IService<TRequest, TResponse>
{
    Task<IEnumerable<TResponse>> GetAllAsync();
    Task<TResponse> GetByIdAsync(int id);
    Task<TResponse> CreateAsync(TRequest request);
    Task<TResponse> UpdateAsync(int id, TRequest request);
    Task DeleteAsync(int id);
}
