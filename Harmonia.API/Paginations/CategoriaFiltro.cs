namespace Harmonia.API.Paginations;

public class CategoriaFiltro : PaginationParameters
{
    /// <summary>
    /// Nome da categoria que será filtrada.
    /// </summary>
    public string? Nome { get; set; }
}
