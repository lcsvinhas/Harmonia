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

    public async Task<IPagedList<Categoria>> GetByFilterPagedAsync(CategoriaFiltro filtro)
    {
        var categorias = _context.Categorias
            .Include(c => c.Instrumentos)
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrEmpty(filtro.Nome))
        {
            categorias = categorias.Where(c => EF.Functions.ILike(c.Nome, $"%{filtro.Nome}%"));
        }

        return await categorias.ToPagedListAsync(filtro.NumeroDaPagina, filtro.ItensPorPagina);
    }

    public async Task<IPagedList<Categoria>> GetPagedAsync(CategoriaParameters param)
    {
        return await _context.Categorias
            .Include(c => c.Instrumentos)
            .AsNoTracking()
            .OrderBy(c => c.CategoriaId)
            .ToPagedListAsync(param.NumeroDaPagina, param.ItensPorPagina);
    }
}
