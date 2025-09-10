using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagementSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedNullableAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VacationApproval_AspNetRoles_ApprovedByRoleId",
                table: "VacationApproval");

            migrationBuilder.DropIndex(
                name: "IX_Employee_UserID",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "BonusDate",
                table: "Bonus");

            migrationBuilder.RenameColumn(
                name: "ApprovedByRoleId",
                table: "VacationApproval",
                newName: "ApprovedByRoleID");

            migrationBuilder.RenameIndex(
                name: "IX_VacationApproval_ApprovedByRoleId",
                table: "VacationApproval",
                newName: "IX_VacationApproval_ApprovedByRoleID");

            migrationBuilder.AlterColumn<decimal>(
                name: "FinalAmount",
                table: "Payroll",
                type: "decimal(9,2)",
                precision: 9,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,2)",
                oldPrecision: 9,
                oldScale: 2);

            migrationBuilder.AlterColumn<int>(
                name: "ApprovedBy",
                table: "Overtime",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AdjustedAt",
                table: "EmployeeProject",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "Bonus",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "AFK_Time",
                table: "Attendance",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_UserID",
                table: "Employee",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_VacationApproval_AspNetRoles_ApprovedByRoleID",
                table: "VacationApproval",
                column: "ApprovedByRoleID",
                principalTable: "AspNetRoles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VacationApproval_AspNetRoles_ApprovedByRoleID",
                table: "VacationApproval");

            migrationBuilder.DropIndex(
                name: "IX_Employee_UserID",
                table: "Employee");

            migrationBuilder.RenameColumn(
                name: "ApprovedByRoleID",
                table: "VacationApproval",
                newName: "ApprovedByRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_VacationApproval_ApprovedByRoleID",
                table: "VacationApproval",
                newName: "IX_VacationApproval_ApprovedByRoleId");

            migrationBuilder.AlterColumn<decimal>(
                name: "FinalAmount",
                table: "Payroll",
                type: "decimal(9,2)",
                precision: 9,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,2)",
                oldPrecision: 9,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ApprovedBy",
                table: "Overtime",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AdjustedAt",
                table: "EmployeeProject",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "Bonus",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BonusDate",
                table: "Bonus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "AFK_Time",
                table: "Attendance",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_UserID",
                table: "Employee",
                column: "UserID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VacationApproval_AspNetRoles_ApprovedByRoleId",
                table: "VacationApproval",
                column: "ApprovedByRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");
        }
    }
}
