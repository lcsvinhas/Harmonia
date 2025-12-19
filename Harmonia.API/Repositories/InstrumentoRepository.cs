using Harmonia.API.Context;
using Harmonia.API.Models;
using Harmonia.API.Paginations;
using Harmonia.Repositories;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Harmonia.API.Repositories;

public class InstrumentoRepository : Repository<Instrumento>, IInstrumentoRepository
{
    public InstrumentoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IPagedList<Instrumento>> GetByFilterPagedAsync(InstrumentoFiltro filtro)
    {
        var instrumentos = _context.Instrumentos
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrEmpty(filtro.Nome))
        {
            instrumentos = instrumentos.Where(i => EF.Functions.ILike(i.Nome, $"%{filtro.Nome}%"));
        }

        if (!string.IsNullOrEmpty(filtro.Marca))
        {
            instrumentos = instrumentos.Where(i => EF.Functions.ILike(i.Marca, $"%{filtro.Marca}%"));
        }

        if (!string.IsNullOrEmpty(filtro.Modelo))
        {
            instrumentos = instrumentos.Where(i => EF.Functions.ILike(i.Nome, $"%{filtro.Nome}%"));
        }

        if (!string.IsNullOrEmpty(filtro.Cor))
        {
            instrumentos = instrumentos.Where(i => i.Cor != null && EF.Functions.ILike(i.Cor, $"%{filtro.Cor}%"));
        }

        return await instrumentos.ToPagedListAsync(filtro.NumeroDaPagina, filtro.ItensPorPagina);
    }

    public async Task<IPagedList<Instrumento>> GetPagedAsync(InstrumentoParameters param)
    {
        return await _context.Instrumentos
            .AsNoTracking()
            .OrderBy(i => i.InstrumentoId)
            .ToPagedListAsync(param.NumeroDaPagina, param.ItensPorPagina);
    }
}