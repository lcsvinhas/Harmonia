namespace Harmonia.API.Models;

public class Instrumento
{
    public int InstrumentoId { get; set; }
    public string? Nome { get; set; }
    public string? Marca { get; set; }
    public string? Modelo { get; set; }
    public string? Descricao { get; set; }
    public string? Cor { get; set; }
    public string? Material { get; set; }
    public int AnoFabricacao { get; set; }
    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }
}
