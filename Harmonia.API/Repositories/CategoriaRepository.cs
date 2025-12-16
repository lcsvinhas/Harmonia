using Harmonia.API.Context;
using Harmonia.API.Models;
using Harmonia.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Harmonia.API.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Categoria>> GetAllWithProductsAsync()
    {
        return await _context.Categorias
            .Include(c => c.Instrumentos)
            .AsNoTracking()
            .ToListAsync();
    }
}
