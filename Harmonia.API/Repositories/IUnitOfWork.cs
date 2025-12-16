namespace Harmonia.API.Repositories;

public interface IUnitOfWork
{
    IInstrumentoRepository InstrumentoRepository { get; }
    ICategoriaRepository CategoriaRepository { get; }
    Task CommitAsync();
}
