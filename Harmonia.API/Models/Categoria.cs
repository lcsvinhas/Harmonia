using System.Collections.ObjectModel;

namespace Harmonia.API.Models;

public class Categoria
{
    public int CategoriaId { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public ICollection<Instrumento>? Instrumentos { get; set; } = new Collection<Instrumento>();
}