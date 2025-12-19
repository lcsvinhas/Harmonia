using Harmonia.API.DTOs;
using Harmonia.API.Paginations;
using X.PagedList;

namespace Harmonia.API.Services;

public interface ICategoriaService : IService<CategoriaRequestDTO, CategoriaResponseDTO>
{
    Task<IEnumerable<CategoriaResponseDTO>> GetAllWithProductsAsync();
    Task<IPagedList<CategoriaResponseDTO>> GetPagedAsync(CategoriaParameters param);
    Task<IPagedList<CategoriaResponseDTO>> GetByFilterPagedAsync(CategoriaFiltro filtro);
}
