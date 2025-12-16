using Harmonia.API.Context;

namespace Harmonia.API.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IInstrumentoRepository? _instrumentoRepo;
    private ICategoriaRepository? _categoriaRepo;
    public AppDbContext _context;
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IInstrumentoRepository InstrumentoRepository
    {
        get
        {
            return _instrumentoRepo ??= new InstrumentoRepository(_context);
        }
    }

    public ICategoriaRepository CategoriaRepository
    {
        get
        {
            return _categoriaRepo ??= new CategoriaRepository(_context);
        }
    }


    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
