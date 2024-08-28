using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManjDb.DataModels.Migrations
{
    /// <inheritdoc />
    public partial class IncludeLastDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastDay",
                table: "ChildInfos",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastDay",
                table: "ChildInfos");
        }
    }
}
