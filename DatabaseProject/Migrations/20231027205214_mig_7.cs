using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseProject.Migrations
{
    /// <inheritdoc />
    public partial class mig_7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adrress_Users_UserId",
                table: "Adrress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adrress",
                table: "Adrress");

            migrationBuilder.RenameTable(
                name: "Adrress",
                newName: "Adrresses");

            migrationBuilder.RenameIndex(
                name: "IX_Adrress_UserId",
                table: "Adrresses",
                newName: "IX_Adrresses_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adrresses",
                table: "Adrresses",
                column: "AdrressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adrresses_Users_UserId",
                table: "Adrresses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adrresses_Users_UserId",
                table: "Adrresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adrresses",
                table: "Adrresses");

            migrationBuilder.RenameTable(
                name: "Adrresses",
                newName: "Adrress");

            migrationBuilder.RenameIndex(
                name: "IX_Adrresses_UserId",
                table: "Adrress",
                newName: "IX_Adrress_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adrress",
                table: "Adrress",
                column: "AdrressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adrress_Users_UserId",
                table: "Adrress",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
