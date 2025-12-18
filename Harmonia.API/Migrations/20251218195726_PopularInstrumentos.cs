using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Harmonia.API.Migrations
{
    /// <inheritdoc />
    public partial class PopularInstrumentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO ""Instrumentos"" (""Nome"", ""Marca"", ""Modelo"", ""Descricao"", ""Cor"", ""Material"", ""AnoFabricacao"", ""CategoriaId"") 
                VALUES ('Violão Clássico C40', 'Yamaha', 'C40', 'Violão clássico com cordas de nylon, ideal para iniciantes e estudantes de música', 'Natural', 'Spruce', 2024, 1)"); 

            migrationBuilder.Sql(@"
                INSERT INTO ""Instrumentos"" (""Nome"", ""Marca"", ""Modelo"", ""Descricao"", ""Cor"", ""Material"", ""AnoFabricacao"", ""CategoriaId"") 
                VALUES ('Violão Folk Aço', 'Tagima', 'Memphis MD-18', 'Violão folk com cordas de aço, som brilhante e projeção potente', 'Sunburst', 'Mogno', 2023, 1)");

            migrationBuilder.Sql(@"
                INSERT INTO ""Instrumentos"" (""Nome"", ""Marca"", ""Modelo"", ""Descricao"", ""Cor"", ""Material"", ""AnoFabricacao"", ""CategoriaId"") 
                VALUES ('Violão Eletroacústico', 'Giannini', 'GF-1D CEQ', 'Violão eletroacústico com equalizador e afinador embutido', 'Preto', 'Cedro', 2024, 1)");

            migrationBuilder.Sql(@"
                INSERT INTO ""Instrumentos"" (""Nome"", ""Marca"", ""Modelo"", ""Descricao"", ""Cor"", ""Material"", ""AnoFabricacao"", ""CategoriaId"") 
                VALUES ('Guitarra Stratocaster', 'Fender', 'Player Series', 'Guitarra elétrica com 3 captadores single coil e tremolo vintage', 'Vermelho', 'Alder', 2023, 2)");

            migrationBuilder.Sql(@"
                INSERT INTO ""Instrumentos"" (""Nome"", ""Marca"", ""Modelo"", ""Descricao"", ""Cor"", ""Material"", ""AnoFabricacao"", ""CategoriaId"") 
                VALUES ('Guitarra Les Paul', 'Epiphone', 'Standard 50s', 'Guitarra elétrica com 2 captadores humbucker, som encorpado e sustain prolongado', 'Dourado', 'Mogno', 2024, 2)"); 

            migrationBuilder.Sql(@"
                INSERT INTO ""Instrumentos"" (""Nome"", ""Marca"", ""Modelo"", ""Descricao"", ""Cor"", ""Material"", ""AnoFabricacao"", ""CategoriaId"") 
                VALUES ('Guitarra Telecaster', 'Tagima', 'TW-55', 'Guitarra elétrica estilo telecaster com timbre brilhante e versátil', 'Azul', 'Basswood', 2023, 2)");

            migrationBuilder.Sql(@"
                INSERT INTO ""Instrumentos"" (""Nome"", ""Marca"", ""Modelo"", ""Descricao"", ""Cor"", ""Material"", ""AnoFabricacao"", ""CategoriaId"") 
                VALUES ('Baixo Precision', 'Fender', 'Player P-Bass', 'Baixo elétrico de 4 cordas com captador split single coil, som poderoso e definido', 'Preto', 'Alder', 2024, 3)");

            migrationBuilder.Sql(@"
                INSERT INTO ""Instrumentos"" (""Nome"", ""Marca"", ""Modelo"", ""Descricao"", ""Cor"", ""Material"", ""AnoFabricacao"", ""CategoriaId"") 
                VALUES ('Baixo Jazz Bass', 'Squier', 'Affinity Series', 'Baixo elétrico de 4 cordas com 2 captadores single coil, som versátil e articulado', 'Sunburst', 'Poplar', 2023, 3)");

            migrationBuilder.Sql(@"
                INSERT INTO ""Instrumentos"" (""Nome"", ""Marca"", ""Modelo"", ""Descricao"", ""Cor"", ""Material"", ""AnoFabricacao"", ""CategoriaId"") 
                VALUES ('Baixo 5 Cordas Ativo', 'Ibanez', 'SR305E', 'Baixo elétrico ativo de 5 cordas com eletrônica Ibanez EQB-IIIS', 'Vermelho', 'Mogno', 2024, 3)"); 

            migrationBuilder.Sql(@"
                INSERT INTO ""Instrumentos"" (""Nome"", ""Marca"", ""Modelo"", ""Descricao"", ""Cor"", ""Material"", ""AnoFabricacao"", ""CategoriaId"") 
                VALUES ('Bateria Acústica 5pç', 'Pearl', 'Export EXX725', 'Bateria acústica completa com 5 tambores, ferragens e pratos inclusos', 'Preto', 'Poplar', 2023, 4)");

            migrationBuilder.Sql(@"
                INSERT INTO ""Instrumentos"" (""Nome"", ""Marca"", ""Modelo"", ""Descricao"", ""Cor"", ""Material"", ""AnoFabricacao"", ""CategoriaId"") 
                VALUES ('Bateria Eletrônica', 'Roland', 'TD-07KV', 'Bateria eletrônica com pads de mesh, módulo TD-07 e sons premium', 'Preto', 'Mesh/ABS', 2024, 4)");

            migrationBuilder.Sql(@"
                INSERT INTO ""Instrumentos"" (""Nome"", ""Marca"", ""Modelo"", ""Descricao"", ""Cor"", ""Material"", ""AnoFabricacao"", ""CategoriaId"") 
                VALUES ('Bateria Infantil 3pç', 'RMV', 'Kids Junior', 'Bateria acústica infantil com 3 tambores, ideal para crianças de 3 a 8 anos', 'Azul', 'Poplar', 2023, 4)");

            migrationBuilder.Sql(@"
                INSERT INTO ""Instrumentos"" (""Nome"", ""Marca"", ""Modelo"", ""Descricao"", ""Cor"", ""Material"", ""AnoFabricacao"", ""CategoriaId"") 
                VALUES ('Piano Digital 88tec', 'Casio', 'CDP-S110', 'Piano digital com 88 teclas sensíveis ao toque e sons realistas de piano acústico', 'Preto', 'Plástico/Metal', 2024, 5)");

            migrationBuilder.Sql(@"
                INSERT INTO ""Instrumentos"" (""Nome"", ""Marca"", ""Modelo"", ""Descricao"", ""Cor"", ""Material"", ""AnoFabricacao"", ""CategoriaId"") 
                VALUES ('Piano de Cauda', 'Yamaha', 'GB1K', 'Piano de cauda acústico compacto com excelente qualidade sonora e acabamento premium', 'Preto Polido', 'Spruce/Mogno', 2023, 5)"); 

            migrationBuilder.Sql(@"
                INSERT INTO ""Instrumentos"" (""Nome"", ""Marca"", ""Modelo"", ""Descricao"", ""Cor"", ""Material"", ""AnoFabricacao"", ""CategoriaId"") 
                VALUES ('Teclado Arranjador', 'Yamaha', 'PSR-E473', 'Teclado arranjador com 61 teclas, 820 vozes e recursos de aprendizado', 'Preto', 'Plástico', 2024, 5)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM ""Instrumentos"" 
                WHERE ""Nome"" IN (
                    'Violão Clássico C40',
                    'Violão Folk Aço',
                    'Violão Eletroacústico',
                    'Guitarra Stratocaster',
                    'Guitarra Les Paul',
                    'Guitarra Telecaster',
                    'Baixo Precision',
                    'Baixo Jazz Bass',
                    'Baixo 5 Cordas Ativo',
                    'Bateria Acústica 5pç',
                    'Bateria Eletrônica',
                    'Bateria Infantil 3pç',
                    'Piano Digital 88tec',
                    'Piano de Cauda',
                    'Teclado Arranjador'
                )");
        }
    }
}
