using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace example.Migrations
{
    /// <inheritdoc />
    public partial class Migracioninicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mascotas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreDeLaMascota = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoDeMascota = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreDelTutor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorreoDelTutor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Celular = table.Column<long>(type: "bigint", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mascotas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mascotas");
        }
    }
}
