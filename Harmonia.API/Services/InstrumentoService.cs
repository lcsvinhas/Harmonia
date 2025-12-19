using AutoMapper;
using Harmonia.API.DTOs;
using Harmonia.API.Exceptions;
using Harmonia.API.Models;
using Harmonia.API.Paginations;
using Harmonia.API.Repositories;
using X.PagedList;

namespace Harmonia.API.Services;

public class InstrumentoService : IInstrumentoService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public InstrumentoService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<InstrumentoResponseDTO>> GetAllAsync()
    {
        var instrumentos = await _unitOfWork.InstrumentoRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<InstrumentoResponseDTO>>(instrumentos);
    }

    public async Task<IPagedList<InstrumentoResponseDTO>> GetPagedAsync(InstrumentoParameters instrumentoParameters)
    {
        var instrumentos = await _unitOfWork.InstrumentoRepository.GetPagedAsync(instrumentoParameters);
        var responseDto = _mapper.Map<IEnumerable<InstrumentoResponseDTO>>(instrumentos);
        return new StaticPagedList<InstrumentoResponseDTO>(responseDto, instrumentos.PageNumber, instrumentos.PageSize, instrumentos.TotalItemCount);
    }

    public async Task<IPagedList<InstrumentoResponseDTO>> GetByModeloPagedAsync(InstrumentoFiltroModelo instrumentoFiltroModelo)
    {
        var instrumentos = await _unitOfWork.InstrumentoRepository.GetByModeloPagedAsync(instrumentoFiltroModelo);
        var responseDto = _mapper.Map<IEnumerable<InstrumentoResponseDTO>>(instrumentos);
        return new StaticPagedList<InstrumentoResponseDTO>(responseDto, instrumentos.PageNumber, instrumentos.PageSize, instrumentos.TotalItemCount);
    }

    public async Task<InstrumentoResponseDTO> GetByIdAsync(int id)
    {
        var instrumento = await _unitOfWork.InstrumentoRepository.GetAsync(i => i.InstrumentoId == id);

        if (instrumento is null)
        {
            throw new NotFoundException($"Instrumento com id '{id}' não encontrado.");
        }

        return _mapper.Map<InstrumentoResponseDTO>(instrumento);
    }

    public async Task<InstrumentoResponseDTO> CreateAsync(InstrumentoRequestDTO request)
    {
        var instrumentoExistente = await _unitOfWork.InstrumentoRepository.GetAsync
        (
        i => i.Nome.ToLower() == request.Nome.ToLower()
        && i.CategoriaId == request.CategoriaId
        );

        if (instrumentoExistente is not null)
        {
            throw new BadRequestException($"Instrumento com nome '{request.Nome}' já existe nesta categoria.");
        }

        var novoInstrumento = _mapper.Map<Instrumento>(request);
        _unitOfWork.InstrumentoRepository.Create(novoInstrumento);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<InstrumentoResponseDTO>(novoInstrumento);
    }

    public async Task<InstrumentoResponseDTO> UpdateAsync(int id, InstrumentoRequestDTO request)
    {
        var instrumentoExistente = await _unitOfWork.InstrumentoRepository.GetAsync(i => i.InstrumentoId == id);

        if (instrumentoExistente is null)
        {
            throw new NotFoundException($"Instrumento com id '{id}' não encontrado.");
        }

        _mapper.Map(request, instrumentoExistente);
        _unitOfWork.InstrumentoRepository.Update(instrumentoExistente);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<InstrumentoResponseDTO>(instrumentoExistente);
    }

    public async Task DeleteAsync(int id)
    {
        var instrumentoExistente = await _unitOfWork.InstrumentoRepository.GetAsync(i => i.InstrumentoId == id);

        if (instrumentoExistente is null)
        {
            throw new NotFoundException($"Instrumento com id '{id}' não encontrado.");
        }

        _unitOfWork.InstrumentoRepository.Delete(instrumentoExistente);
        await _unitOfWork.CommitAsync();
    }
}
