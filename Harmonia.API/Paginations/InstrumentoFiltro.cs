namespace Harmonia.API.Paginations;

public class InstrumentoFiltro : PaginationParameters
{
    public string? Nome { get; set; }
    public string? Marca { get; set; }
    public string? Modelo { get; set; }
    public string? Cor { get; set; }
}
