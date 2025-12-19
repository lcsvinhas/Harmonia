using AutoMapper;
using Harmonia.API.DTOs;
using Harmonia.API.Exceptions;
using Harmonia.API.Paginations;
using Harmonia.API.Repositories;
using X.PagedList;

namespace Harmonia.API.Services;

public class CategoriaService : ICategoriaService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoriaService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoriaResponseDTO>> GetAllWithProductsAsync()
    {
        var categorias = await _unitOfWork.CategoriaRepository.GetAllWithProductsAsync();
        return _mapper.Map<IEnumerable<CategoriaResponseDTO>>(categorias);
    }

    public async Task<IEnumerable<CategoriaResponseDTO>> GetAllAsync()
    {
        var categorias = await _unitOfWork.CategoriaRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CategoriaResponseDTO>>(categorias);
    }

    public async Task<IPagedList<CategoriaResponseDTO>> GetPagedAsync(CategoriaParameters categoriaParameters)
    {
        var categorias = await _unitOfWork.CategoriaRepository.GetPagedAsync(categoriaParameters);
        var responseDto = _mapper.Map<IEnumerable<CategoriaResponseDTO>>(categorias);
        return new StaticPagedList<CategoriaResponseDTO>(responseDto, categorias.PageNumber, categorias.PageSize, categorias.TotalItemCount);
    }

    public async Task<IPagedList<CategoriaResponseDTO>> GetByNamePagedAsync(CategoriaFiltroNome categoriaFiltroNome)
    {
        var categorias = await _unitOfWork.CategoriaRepository.GetByNomePagedAsync(categoriaFiltroNome);
        var responseDto = _mapper.Map<IEnumerable<CategoriaResponseDTO>>(categorias);
        return new StaticPagedList<CategoriaResponseDTO>(responseDto, categorias.PageNumber, categorias.PageSize, categorias.TotalItemCount);
    }

    public async Task<CategoriaResponseDTO> GetByIdAsync(int id)
    {
        var categoria = await _unitOfWork.CategoriaRepository.GetAsync(c => c.CategoriaId == id);

        if (categoria is null)
        {
            throw new NotFoundException($"Categoria com id '{id}' não encontrada.");
        }

        return _mapper.Map<CategoriaResponseDTO>(categoria);
    }

    public async Task<CategoriaResponseDTO> CreateAsync(CategoriaRequestDTO request)
    {
        var categoriaExistente = await _unitOfWork.CategoriaRepository.GetAsync(c => c.Nome.ToLower() == request.Nome.ToLower());

        if (categoriaExistente is not null)
        {
            throw new BadRequestException($"Categoria com nome '{request.Nome}' já existe.");
        }

        var novaCategoria = _mapper.Map<Models.Categoria>(request);
        _unitOfWork.CategoriaRepository.Create(novaCategoria);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<CategoriaResponseDTO>(novaCategoria);
    }

    public async Task<CategoriaResponseDTO> UpdateAsync(int id, CategoriaRequestDTO request)
    {
        var categoriaExistente = await _unitOfWork.CategoriaRepository.GetAsync(c => c.CategoriaId == id);

        if (categoriaExistente is null)
        {
            throw new NotFoundException($"Categoria com id '{id}' não encontrada.");
        }

        _mapper.Map(request, categoriaExistente);
        _unitOfWork.CategoriaRepository.Update(categoriaExistente);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<CategoriaResponseDTO>(categoriaExistente);
    }
    public async Task DeleteAsync(int id)
    {
        var categoriaExistente = await _unitOfWork.CategoriaRepository.GetAsync(c => c.CategoriaId == id);

        if (categoriaExistente is null)
        {
            throw new NotFoundException($"Categoria com id '{id}' não encontrada.");
        }

        _unitOfWork.CategoriaRepository.Delete(categoriaExistente);
        await _unitOfWork.CommitAsync();
    }
}
