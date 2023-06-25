using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    idcategoria = table.Column<int>(name: "id_categoria", type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.idcategoria);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pelicula",
                columns: table => new
                {
                    idpelicula = table.Column<int>(name: "id_pelicula", type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    titulo = table.Column<string>(type: "longtext", nullable: false),
                    descripcion = table.Column<string>(type: "longtext", nullable: false),
                    director = table.Column<string>(type: "longtext", nullable: false),
                    year = table.Column<int>(type: "int", nullable: false),
                    duracion = table.Column<int>(type: "int", nullable: false),
                    fotoPelicula = table.Column<string>(type: "longtext", nullable: false),
                    idcategoria = table.Column<int>(name: "id_categoria", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pelicula", x => x.idpelicula);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    idusuario = table.Column<int>(name: "id_usuario", type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "longtext", nullable: false),
                    correoelectronico = table.Column<string>(name: "correo_electronico", type: "longtext", nullable: false),
                    password = table.Column<string>(type: "longtext", nullable: false),
                    tipo = table.Column<string>(type: "longtext", nullable: false),
                    fotoUsuario = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.idusuario);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PeliculaUsuario",
                columns: table => new
                {
                    idpelicula = table.Column<int>(name: "id_pelicula", type: "int", nullable: false),
                    idusuario = table.Column<int>(name: "id_usuario", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeliculaUsuario", x => new { x.idpelicula, x.idusuario });
                    table.ForeignKey(
                        name: "FK_PeliculaUsuario_Pelicula_id_pelicula",
                        column: x => x.idpelicula,
                        principalTable: "Pelicula",
                        principalColumn: "id_pelicula",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeliculaUsuario_Usuario_id_usuario",
                        column: x => x.idusuario,
                        principalTable: "Usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

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
