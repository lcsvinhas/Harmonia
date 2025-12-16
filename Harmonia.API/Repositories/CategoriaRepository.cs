using Harmonia.API.Context;
using Harmonia.API.Models;
using Harmonia.Repositories;

namespace Harmonia.API.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {
    }
}
