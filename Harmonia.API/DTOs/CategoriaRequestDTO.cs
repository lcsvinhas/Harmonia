using System.ComponentModel.DataAnnotations;

namespace Harmonia.API.DTOs;

public class CategoriaRequestDTO
{
    [Required(ErrorMessage = "Nome é obrigatório.")]
    [MaxLength(30, ErrorMessage = "Nome deve ter no máximo 30 caracteres.")]
    public string Nome { get; set; } = null!;

    [MaxLength(300, ErrorMessage = "Descrição deve ter no máximo 300 caracteres.")]
    public string? Descricao { get; set; }
}
