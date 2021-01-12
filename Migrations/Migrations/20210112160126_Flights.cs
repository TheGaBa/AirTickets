using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrations.Migrations
{
    public partial class Flights : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    userId = table.Column<Guid>(nullable: true),
                    departureDate = table.Column<DateTime>(nullable: true),
                    arrivalDate = table.Column<DateTime>(nullable: true),
                    departureCountry = table.Column<string>(nullable: true),
                    arrivalCountry = table.Column<string>(nullable: true),
                    departureCity = table.Column<string>(nullable: true),
                    arrivalCity = table.Column<string>(nullable: true),
                    price = table.Column<double>(nullable: true),
                    adult = table.Column<bool>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flights");
        }
}
