using Harmonia.API.Context;
using Harmonia.API.Models;
using Harmonia.API.Paginations;
using Harmonia.Repositories;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

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

    public async Task<IPagedList<Categoria>> GetByNomePagedAsync(CategoriaFiltroNome categoriaFiltroNome)
    {
        var categorias = await GetAllAsync();
        if (!string.IsNullOrEmpty(categoriaFiltroNome.Nome))
        {
            categorias = categorias.Where(c => c.Nome.Contains(categoriaFiltroNome.Nome));
        }
        return await categorias.ToPagedListAsync(categoriaFiltroNome.NumeroDaPagina, categoriaFiltroNome.ItensPorPagina);
    }

    public async Task<IPagedList<Categoria>> GetPagedAsync(CategoriaParameters categoriaParameters)
    {
        var categorias = await GetAllAsync();
        var categoriasOrdenadas = categorias.OrderBy(c => c.CategoriaId).AsQueryable();
        return await categoriasOrdenadas.ToPagedListAsync(categoriaParameters.NumeroDaPagina, categoriaParameters.ItensPorPagina);
    }
}
