using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManjDb.DataModels.Migrations
{
    /// <inheritdoc />
    public partial class MigrationForReleasev1_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RawData");

            migrationBuilder.DropTable(
                name: "TempChildren");

            migrationBuilder.CreateTable(
                name: "ChildInfos",
                columns: table => new
                {
                    ChildId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    ProfilePhoto = table.Column<string>(type: "TEXT", nullable: true),
                    BirthDate = table.Column<string>(type: "TEXT", nullable: true),
                    MiddleName = table.Column<string>(type: "TEXT", nullable: true),
                    Gender = table.Column<string>(type: "TEXT", nullable: true),
                    HoursString = table.Column<string>(type: "TEXT", nullable: true),
                    DominantLanguage = table.Column<string>(type: "TEXT", nullable: true),
                    Allergies = table.Column<string>(type: "TEXT", nullable: true),
                    Program = table.Column<string>(type: "TEXT", nullable: true),
                    FirstDay = table.Column<string>(type: "TEXT", nullable: true),
                    ApprovedAdultsString = table.Column<string>(type: "TEXT", nullable: true),
                    EmergencyContactsString = table.Column<string>(type: "TEXT", nullable: true),
                    ClassroomIdsString = table.Column<string>(type: "TEXT", nullable: true),
                    ParentIdsString = table.Column<string>(type: "TEXT", nullable: true),
                    FormsRawJson = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildInfos", x => x.ChildId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChildInfos");

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
    }
}
