using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeManagementSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class addeddefaultOvertimeRulevaluesindatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "OvertimeRule",
                columns: new[] { "ID", "DayType", "Multiplier" },
                values: new object[,]
                {
                    { 1, "WeekDay", 1.5m },
                    { 2, "WeekEnd", 2.0m },
                    { 3, "Holiday", 2.0m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OvertimeRule",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OvertimeRule",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OvertimeRule",
                keyColumn: "ID",
                keyValue: 3);
        }
    }
}
