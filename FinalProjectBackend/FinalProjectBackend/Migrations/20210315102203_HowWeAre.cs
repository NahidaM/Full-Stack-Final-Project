using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProjectBackend.Migrations
{
    public partial class HowWeAre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "HowWeAres",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MartryInfo",
                table: "HowWeAres",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "HowWeAres");

            migrationBuilder.DropColumn(
                name: "MartryInfo",
                table: "HowWeAres");
        }
    }
}
