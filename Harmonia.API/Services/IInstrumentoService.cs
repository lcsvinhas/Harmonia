using Harmonia.API.DTOs;
using Harmonia.API.Paginations;
using X.PagedList;

namespace Harmonia.API.Services;

public interface IInstrumentoService : IService<InstrumentoRequestDTO, InstrumentoResponseDTO>
{
    Task<IPagedList<InstrumentoResponseDTO>> GetByModeloPagedAsync(InstrumentoFiltroModelo instrumentoFiltroModelo);
    Task<IPagedList<InstrumentoResponseDTO>> GetPagedAsync(InstrumentoParameters instrumentoParameters);
}
