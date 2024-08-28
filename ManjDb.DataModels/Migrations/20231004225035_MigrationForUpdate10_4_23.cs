using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManjDb.DataModels.Migrations
{
    /// <inheritdoc />
    public partial class MigrationForUpdate10_4_23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormTemplateIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EnrollmentContractId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmergencyInformationId = table.Column<int>(type: "INTEGER", nullable: false),
                    ApprovedPickupId = table.Column<int>(type: "INTEGER", nullable: false),
                    PhotoReleaseId = table.Column<int>(type: "INTEGER", nullable: false),
                    AnimalPermissionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormTemplateIds", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormTemplateIds");
        }
    }
}
