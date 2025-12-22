namespace Harmonia.API.Paginations;

public class InstrumentoFiltro : PaginationParameters
{
    /// <summary>
    /// Nome do instrumento que será filtrado.
    /// </summary>
    public string? Nome { get; set; }

    /// <summary>
    /// Marca do instrumento que será filtrado.
    /// </summary>
    public string? Marca { get; set; }

    /// <summary>
    /// Modelo do instrumento que será filtrado.
    /// </summary>
    public string? Modelo { get; set; }

    /// <summary>
    /// Cor do instrumento que será filtrado.
    /// </summary>
    public string? Cor { get; set; }
}
