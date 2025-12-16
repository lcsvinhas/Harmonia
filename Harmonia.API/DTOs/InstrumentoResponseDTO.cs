namespace Harmonia.API.DTOs;

public record InstrumentoResponseDTO
(
    int InstrumentoId,
    string Nome,
    string Marca,
    string Modelo,
    string? Descricao,
    string? Cor,
    string? Material,
    int? AnoFabricacao,
    int CategoriaId
);
