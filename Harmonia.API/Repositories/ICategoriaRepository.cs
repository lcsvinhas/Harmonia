using Harmonia.API.Models;

namespace Harmonia.API.Repositories;

public interface ICategoriaRepository : IRepository<Categoria>
{
    Task<IEnumerable<Categoria>> GetAllWithProductsAsync();
}
