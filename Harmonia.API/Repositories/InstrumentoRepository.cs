using Harmonia.API.Context;
using Harmonia.API.Models;
using Harmonia.Repositories;

namespace Harmonia.API.Repositories;

internal class InstrumentoRepository : Repository<Instrumento>, IInstrumentoRepository
{
    public InstrumentoRepository(AppDbContext context) : base(context)
    {
    }
}