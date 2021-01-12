using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrations.Migrations
{
    public partial class ListOfUserThings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "ListsOfUserThings",
               columns: table => new
               {
                   id = table.Column<Guid>(nullable: false),
                   userId = table.Column<Guid>(nullable: true),
                   arrayOfThings = table.Column<string>(nullable: true),
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_ListsOfUserThings", x => x.id);
               });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "ListsOfUserThings");
        }
    }
}
