using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class hash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "id_usuario",
                keyValue: 1,
                column: "password",
                value: "AQAAAAIAAYagAAAAELNLE8DNCig/wQtzXDLQ+sCqtBMe6StCvKb69rQIMMT3EFp/6iOeFBNQwX5hcWvcRg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "id_usuario",
                keyValue: 1,
                column: "password",
                value: "admin");
        }
    }
}
