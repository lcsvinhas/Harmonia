using Harmonia.API.Models;

namespace Harmonia.API.DTOs;

public record CategoriaResponseDTO
(
    int CategoriaId,
    string Nome,
    string? Descricao,
    ICollection<Instrumento>? Instrumentos
);
