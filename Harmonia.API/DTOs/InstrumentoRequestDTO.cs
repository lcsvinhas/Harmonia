using System.ComponentModel.DataAnnotations;

namespace Harmonia.API.DTOs;

public class InstrumentoRequestDTO
{
    [Required(ErrorMessage = "Nome é obrigatório.")]
    [MaxLength(50, ErrorMessage = "Nome deve ter no máximo 50 caracteres.")]
    public string Nome { get; set; } = null!;

    [Required(ErrorMessage = "Marca é obrigatório.")]
    [MaxLength(20, ErrorMessage = "Marca deve ter no máximo 20 caracteres.")]
    public string Marca { get; set; } = null!;

    [Required(ErrorMessage = "Modelo é obrigatório.")]
    [MaxLength(20, ErrorMessage = "Modelo deve ter no máximo 20 caracteres.")]
    public string Modelo { get; set; } = null!;

    [MaxLength(300, ErrorMessage = "Descrição deve ter no máximo 300 caracteres.")]
    public string? Descricao { get; set; }

    [MaxLength(20, ErrorMessage = "Cor deve ter no máximo 20 caracteres.")]
    public string? Cor { get; set; }

    [MaxLength(20, ErrorMessage = "Material deve ter no máximo 20 caracteres.")]
    public string? Material { get; set; }

    public int? AnoFabricacao { get; set; }

    [Required(ErrorMessage = "CategoriaId é obrigatório.")]
    public int CategoriaId { get; set; }
}
