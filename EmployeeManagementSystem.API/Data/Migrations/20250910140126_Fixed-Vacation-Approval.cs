using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagementSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class FixedVacationApproval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Overtime_Employee_ApprovedBy",
                table: "Overtime");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VacationApproval",
                table: "VacationApproval");

            migrationBuilder.DropColumn(
                name: "ApprovedByRole",
                table: "VacationApproval");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "VacationApproval");

            migrationBuilder.DropColumn(
                name: "RoleLevel",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Decision",
                table: "VacationApproval",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApprovalDate",
                table: "VacationApproval",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "ApprovedByRoleId",
                table: "VacationApproval",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

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
                name: "EndTime",
                table: "EmployeeProject",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckOut",
                table: "Attendance",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VacationApproval",
                table: "VacationApproval",
                columns: new[] { "RequestID", "ApprovedByRoleId" });

            migrationBuilder.CreateIndex(
                name: "IX_VacationApproval_ApprovedByRoleId",
                table: "VacationApproval",
                column: "ApprovedByRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Overtime_Employee_ApprovedBy",
                table: "Overtime",
                column: "ApprovedBy",
                principalTable: "Employee",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_VacationApproval_AspNetRoles_ApprovedByRoleId",
                table: "VacationApproval",
                column: "ApprovedByRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Overtime_Employee_ApprovedBy",
                table: "Overtime");

            migrationBuilder.DropForeignKey(
                name: "FK_VacationApproval_AspNetRoles_ApprovedByRoleId",
                table: "VacationApproval");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VacationApproval",
                table: "VacationApproval");

            migrationBuilder.DropIndex(
                name: "IX_VacationApproval_ApprovedByRoleId",
                table: "VacationApproval");

            migrationBuilder.DropColumn(
                name: "ApprovedByRoleId",
                table: "VacationApproval");

            migrationBuilder.AlterColumn<string>(
                name: "Decision",
                table: "VacationApproval",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApprovalDate",
                table: "VacationApproval",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedByRole",
                table: "VacationApproval",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "VacationApproval",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ApprovedBy",
                table: "Overtime",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "EmployeeProject",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleLevel",
                table: "Employee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckOut",
                table: "Attendance",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VacationApproval",
                table: "VacationApproval",
                columns: new[] { "RequestID", "ApprovedByRole" });

            migrationBuilder.AddForeignKey(
                name: "FK_Overtime_Employee_ApprovedBy",
                table: "Overtime",
                column: "ApprovedBy",
                principalTable: "Employee",
                principalColumn: "EmployeeID");
        }
    }
}
