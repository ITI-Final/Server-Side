using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OlxDataAccess.Migrations
{
    public partial class editcompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_company_user",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_Owner",
                table: "Company");

            migrationBuilder.AlterColumn<int>(
                name: "Owner",
                table: "Company",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "OwnerID",
                table: "Company",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Company_OwnerID",
                table: "Company",
                column: "OwnerID");

            migrationBuilder.AddForeignKey(
                name: "FK_company_user",
                table: "Company",
                column: "OwnerID",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_company_user",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_OwnerID",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "Company");

            migrationBuilder.AlterColumn<int>(
                name: "Owner",
                table: "Company",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_Owner",
                table: "Company",
                column: "Owner");

            migrationBuilder.AddForeignKey(
                name: "FK_company_user",
                table: "Company",
                column: "Owner",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
