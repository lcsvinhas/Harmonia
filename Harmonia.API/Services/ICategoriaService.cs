using Harmonia.API.DTOs;

namespace Harmonia.API.Services;

public interface ICategoriaService : IService<CategoriaRequestDTO, CategoriaResponseDTO>
{
    Task<IEnumerable<CategoriaResponseDTO>> GetAllWithProductsAsync();
}
