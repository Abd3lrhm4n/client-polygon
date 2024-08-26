using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedAttemptColumnToEmailQueue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "EmailQueues",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 25, 3, 17, 5, 942, DateTimeKind.Utc).AddTicks(6330));

            migrationBuilder.AddColumn<int>(
                name: "Attempts",
                table: "EmailQueues",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attempts",
                table: "EmailQueues");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "EmailQueues",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 25, 3, 17, 5, 942, DateTimeKind.Utc).AddTicks(6330),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
