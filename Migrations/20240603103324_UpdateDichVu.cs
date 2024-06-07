using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NhaKhoaQuangVu.Migrations
{
    public partial class UpdateDichVu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {      
            migrationBuilder.AddColumn<string>(
                name: "ChiTiet",
                table: "BangGias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChiTiet",
                table: "BangGias");           
        }
    }
}
