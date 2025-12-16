namespace Harmonia.API.Models;

public class Instrumento
{
    public int InstrumentoId { get; set; }
    public string Nome { get; set; } = null!;
    public string Marca { get; set; } = null!;
    public string Modelo { get; set; } = null!;
    public string? Descricao { get; set; }
    public string? Cor { get; set; }
    public string? Material { get; set; }
    public int? AnoFabricacao { get; set; }
    public int CategoriaId { get; set; }
    public Categoria Categoria { get; set; } = null!;
}
