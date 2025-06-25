using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class RelacionPeliculaCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Pelicula_id_categoria",
                table: "Pelicula",
                column: "id_categoria");

            migrationBuilder.AddForeignKey(
                name: "FK_Pelicula_Categoria_id_categoria",
                table: "Pelicula",
                column: "id_categoria",
                principalTable: "Categoria",
                principalColumn: "id_categoria",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pelicula_Categoria_id_categoria",
                table: "Pelicula");

            migrationBuilder.DropIndex(
                name: "IX_Pelicula_id_categoria",
                table: "Pelicula");
        }
    }
}
