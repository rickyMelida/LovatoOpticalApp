using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LovatoOpticalApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedPrescriptionIssueDateColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PrescriptionIssueDate",
                table: "Recipes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrescriptionIssueDate",
                table: "Recipes");
        }
    }
}
