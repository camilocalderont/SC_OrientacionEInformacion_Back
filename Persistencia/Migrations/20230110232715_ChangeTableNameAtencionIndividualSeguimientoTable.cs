using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistencia.Migrations
{
    public partial class ChangeTableNameAtencionIndividualSeguimientoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AtencionSeguimiento",
                table: "AtencionSeguimiento");

            migrationBuilder.RenameTable(
                name: "AtencionSeguimiento",
                newName: "AtencionIndividualSeguimiento");

            migrationBuilder.RenameIndex(
                name: "IX_AtencionSeguimiento_AtencionIndividualId",
                table: "AtencionIndividualSeguimiento",
                newName: "IX_AtencionIndividualSeguimiento_AtencionIndividualId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AtencionIndividualSeguimiento",
                table: "AtencionIndividualSeguimiento",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AtencionIndividualSeguimiento",
                table: "AtencionIndividualSeguimiento");

            migrationBuilder.RenameTable(
                name: "AtencionIndividualSeguimiento",
                newName: "AtencionSeguimiento");

            migrationBuilder.RenameIndex(
                name: "IX_AtencionIndividualSeguimiento_AtencionIndividualId",
                table: "AtencionSeguimiento",
                newName: "IX_AtencionSeguimiento_AtencionIndividualId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AtencionSeguimiento",
                table: "AtencionSeguimiento",
                column: "Id");
        }
    }
}
