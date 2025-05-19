using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class migracioninicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    id_categoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.id_categoria);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pelicula",
                columns: table => new
                {
                    id_pelicula = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    titulo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    director = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    year = table.Column<int>(type: "int", nullable: false),
                    duracion = table.Column<int>(type: "int", nullable: false),
                    fotoPelicula = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id_categoria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pelicula", x => x.id_pelicula);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    correo_electronico = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tipo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fotoUsuario = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.id_usuario);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PeliculaUsuario",
                columns: table => new
                {
                    id_pelicula = table.Column<int>(type: "int", nullable: false),
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeliculaUsuario", x => new { x.id_pelicula, x.id_usuario });
                    table.ForeignKey(
                        name: "FK_PeliculaUsuario_Pelicula_id_pelicula",
                        column: x => x.id_pelicula,
                        principalTable: "Pelicula",
                        principalColumn: "id_pelicula",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeliculaUsuario_Usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "id_categoria", "nombre" },
                values: new object[,]
                {
                    { 1, "Animación" },
                    { 2, "Crimen" },
                    { 3, "Familia" },
                    { 4, "Misterio" },
                    { 5, "Suspenso" },
                    { 6, "Terror" },
                    { 7, "Romance" },
                    { 8, "Drama" },
                    { 9, "Ciencia Ficción" },
                    { 10, "Acción" }
                });

            migrationBuilder.InsertData(
                table: "Pelicula",
                columns: new[] { "id_pelicula", "descripcion", "director", "duracion", "fotoPelicula", "id_categoria", "titulo", "year" },
                values: new object[,]
                {
                    { 1, "Un mafioso intenta mantener el control de su imperio criminal mientras protege a su familia.", "Francis Ford Coppola", 175, "string", 2, "El Padrino", 1972 },
                    { 2, "Un grupo de juguetes cobra vida cuando los humanos no están presentes.", "John Lasseter", 81, "string", 1, "Toy Story", 1995 },
                    { 3, "Los Vengadores se unen para luchar contra Thanos y restaurar el universo.", "Anthony Russo, Joe Russo", 181, "string", 3, "Avengers: Endgame", 2019 }
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "id_usuario", "correo_electronico", "fotoUsuario", "nombre", "password", "tipo" },
                values: new object[] { 1, "admin@gmail.com", "string", "admin", "admin", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_PeliculaUsuario_id_usuario",
                table: "PeliculaUsuario",
                column: "id_usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "PeliculaUsuario");

            migrationBuilder.DropTable(
                name: "Pelicula");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
