using Harmonia.API.Models;
using Harmonia.API.Paginations;
using X.PagedList;

namespace Harmonia.API.Repositories;

public interface ICategoriaRepository : IRepository<Categoria>
{
    Task<IEnumerable<Categoria>> GetAllWithProductsAsync();
    Task<IPagedList<Categoria>> GetPagedAsync(CategoriaParameters categoriaParameters);
    Task<IPagedList<Categoria>> GetByNomePagedAsync(CategoriaFiltroNome categoriaFiltroNome);
}
