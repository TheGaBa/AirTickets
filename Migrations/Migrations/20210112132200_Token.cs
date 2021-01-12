using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrations.Migrations
{
    public partial class Token : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Tokens",
               columns: table => new
               {
                   id = table.Column<Guid>(nullable: false),
                  
                   token = table.Column<string>(nullable: true),
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Token", x => x.id);
               });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tokens");
        }
    }
}
