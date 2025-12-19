using Harmonia.API.Context;
using Harmonia.API.Models;
using Harmonia.API.Paginations;
using Harmonia.Repositories;
using X.PagedList;

namespace Harmonia.API.Repositories;

public class InstrumentoRepository : Repository<Instrumento>, IInstrumentoRepository
{
    public InstrumentoRepository(AppDbContext context) : base(context)
    {
    }
    public async Task<IPagedList<Instrumento>> GetByModeloPagedAsync(InstrumentoFiltroModelo instrumentoFiltroModelo)
    {
        var instrumentos = await GetAllAsync();

        if (!string.IsNullOrEmpty(instrumentoFiltroModelo.Modelo))
        {
            instrumentos = instrumentos.Where(i => i.Nome.Contains(instrumentoFiltroModelo.Modelo));
        }

        return await instrumentos.ToPagedListAsync(instrumentoFiltroModelo.NumeroDaPagina, instrumentoFiltroModelo.ItensPorPagina);
    }

    public async Task<IPagedList<Instrumento>> GetPagedAsync(InstrumentoParameters instrumentoParameters)
    {
        var instrumentos = await GetAllAsync();
        var instrumentosOrdenados = instrumentos.OrderBy(i => i.InstrumentoId).AsQueryable();
        return await instrumentosOrdenados.ToPagedListAsync(instrumentoParameters.NumeroDaPagina, instrumentoParameters.ItensPorPagina);
    }
}