using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NhaKhoaQuangVu.Migrations
{
    public partial class dbDatHenNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_datHens_BangGias_BangGiaMaDichVu",
                table: "datHens");

            migrationBuilder.DropIndex(
                name: "IX_datHens_BangGiaMaDichVu",
                table: "datHens");

            migrationBuilder.DropColumn(
                name: "BangGiaMaDichVu",
                table: "datHens");

            migrationBuilder.AddColumn<string>(
                name: "TrangThai",
                table: "datHens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "datHens");

            migrationBuilder.AddColumn<int>(
                name: "BangGiaMaDichVu",
                table: "datHens",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_datHens_BangGiaMaDichVu",
                table: "datHens",
                column: "BangGiaMaDichVu");

            migrationBuilder.AddForeignKey(
                name: "FK_datHens_BangGias_BangGiaMaDichVu",
                table: "datHens",
                column: "BangGiaMaDichVu",
                principalTable: "BangGias",
                principalColumn: "MaDichVu");
        }
    }
}
