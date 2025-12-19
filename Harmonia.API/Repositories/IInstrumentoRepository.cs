using Harmonia.API.Models;
using Harmonia.API.Paginations;
using X.PagedList;

namespace Harmonia.API.Repositories;

public interface IInstrumentoRepository : IRepository<Instrumento>
{
    Task<IPagedList<Instrumento>> GetPagedAsync(InstrumentoParameters instrumentoParameters);
    Task<IPagedList<Instrumento>> GetByModeloPagedAsync(InstrumentoFiltroModelo instrumentoFiltroModelo);
}