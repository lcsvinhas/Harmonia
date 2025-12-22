namespace Harmonia.API.Paginations;

public class PaginationParameters
{
    const int maxItensPorPagina = 50;

    /// <summary>
    /// Quantidade de itens por página (máximo de 50).
    /// </summary>
    public int ItensPorPagina
    {
        get { return _maxItensPorPagina; }
        set { _maxItensPorPagina = (value > maxItensPorPagina) ? maxItensPorPagina : value; }
    }

    /// <summary>
    /// Número da página que deve retornar.
    /// </summary>
    public int NumeroDaPagina { get; set; } = 1;
    private int _maxItensPorPagina = maxItensPorPagina;
}
