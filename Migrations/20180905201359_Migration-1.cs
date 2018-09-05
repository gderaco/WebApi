using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AndreaDipreApi.Migrations
{
    public partial class Migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Channels",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false, defaultValueSql: "hex(randomblob(16))"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Karmas",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false, defaultValueSql: "hex(randomblob(16))"),
                    Name = table.Column<string>(nullable: true),
                    Score = table.Column<int>(nullable: false),
                    ChannelId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Karmas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Channels");

            migrationBuilder.DropTable(
                name: "Karmas");
        }
    }
}
