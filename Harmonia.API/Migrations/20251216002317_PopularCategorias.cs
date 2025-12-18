using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Harmonia.API.Migrations
{
    /// <inheritdoc />
    public partial class PopularCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO ""Categorias"" (""Nome"", ""Descricao"") VALUES ('Violão', 'Instrumento musical de cordas dedilhadas que se presta tanto para o acompanhamento de cantores como para o solo.')");
            migrationBuilder.Sql(@"INSERT INTO ""Categorias"" (""Nome"", ""Descricao"") VALUES ('Guitarra', 'Instrumento musical de cordas pinçadas e caixa de ressonância de fundo chato, de origem oriental.')");
            migrationBuilder.Sql(@"INSERT INTO ""Categorias"" (""Nome"", ""Descricao"") VALUES ('Baixo', 'Instrumento musical da família das guitarras, que apresenta características harmônicas e melódicas mais graves. Pode ser acústico ou elétrico, sendo o baixo acústico consideravelmente maior que o elétrico.')");
            migrationBuilder.Sql(@"INSERT INTO ""Categorias"" (""Nome"", ""Descricao"") VALUES ('Bateria', 'Conjunto articulado dos instrumentos de percussão, como bombo, pratos, caixa, caixeta e vassourinha, tocado por um só músico.')");
            migrationBuilder.Sql(@"INSERT INTO ""Categorias"" (""Nome"", ""Descricao"") VALUES ('Piano', 'Instrumento muito desenvolvido e o único que reproduz ao mesmo tempo melodia e harmonia, tendo a capacidade de cobrir quase todos os sons usados na música, além de oferecer uma extraordinária variedade de notas suaves ou fortes, com maior ou menor rapidez, e belos efeitos sonoros.')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM ""Categorias"" WHERE Nome IN ('Violão', 'Guitarra', 'Baixo', 'Bateria', 'Piano')");
        }
    }
}
