namespace Harmonia.API.Paginations;

public class PaginationParameters
{
    const int maxItensPorPagina = 50;
    public int ItensPorPagina
    {
        get { return _maxItensPorPagina; }
        set { _maxItensPorPagina = (value > maxItensPorPagina) ? maxItensPorPagina : value; }
    }
    public int NumeroDaPagina { get; set; } = 1;
    private int _maxItensPorPagina = maxItensPorPagina;
}
