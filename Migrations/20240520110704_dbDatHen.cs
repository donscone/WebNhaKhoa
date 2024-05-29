using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NhaKhoaQuangVu.Migrations
{
    public partial class dbDatHen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "datHens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaDichVu = table.Column<int>(type: "int", nullable: false),
                    BangGiaMaDichVu = table.Column<int>(type: "int", nullable: true),
                    NgayHen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GioHen = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_datHens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_datHens_BangGias_BangGiaMaDichVu",
                        column: x => x.BangGiaMaDichVu,
                        principalTable: "BangGias",
                        principalColumn: "MaDichVu");
                });

            migrationBuilder.CreateIndex(
                name: "IX_datHens_BangGiaMaDichVu",
                table: "datHens",
                column: "BangGiaMaDichVu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "datHens");
        }
    }
}
