using AutoMapper;
using Harmonia.API.Models;

namespace Harmonia.API.DTOs.Mappings;

public class DTOMappingProfile : Profile
{
    public DTOMappingProfile()
    {
        CreateMap<InstrumentoRequestDTO, Instrumento>();
        CreateMap<Instrumento, InstrumentoResponseDTO>();

        CreateMap<CategoriaRequestDTO, Categoria>();
        CreateMap<Categoria, CategoriaResponseDTO>();
    }
}
