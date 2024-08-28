using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManjDb.DataModels.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RawData",
                columns: table => new
                {
                    RawDataId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChildId = table.Column<int>(type: "INTEGER", nullable: false),
                    FormTemplateId = table.Column<int>(type: "INTEGER", nullable: true),
                    JsonData = table.Column<string>(type: "TEXT", nullable: true),
                    RetrievedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawData", x => x.RawDataId);
                });

            migrationBuilder.CreateTable(
                name: "TempChildren",
                columns: table => new
                {
                    TempChildId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempChildren", x => x.TempChildId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RawData");

            migrationBuilder.DropTable(
                name: "TempChildren");
        }
    }
}
