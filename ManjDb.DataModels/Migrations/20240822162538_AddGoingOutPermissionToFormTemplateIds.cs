using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManjDb.DataModels.Migrations
{
    /// <inheritdoc />
    public partial class AddGoingOutPermissionToFormTemplateIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GoingOutPermissionId",
                table: "FormTemplateIds",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoingOutPermissionId",
                table: "FormTemplateIds");
        }
    }
}
