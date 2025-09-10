using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagementSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class DefaultedtoOnDeleteNoActionforall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AFKEvent_Attendance_SessionID",
                table: "AFKEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_AFKEvent_Employee_EmployeeID",
                table: "AFKEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Employee_EmployeeID",
                table: "Attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_Bonus_BonusType_BonusTypeID",
                table: "Bonus");

            migrationBuilder.DropForeignKey(
                name: "FK_Bonus_Employee_EmployeeID",
                table: "Bonus");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_AspNetUsers_UserID",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_VacationLevel_Vacation_Level_ID",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDocument_Employee_EmployeeID",
                table: "EmployeeDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProject_Attendance_SessionID",
                table: "EmployeeProject");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProject_Employee_EmployeeID",
                table: "EmployeeProject");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProject_Project_ProjectID",
                table: "EmployeeProject");

            migrationBuilder.DropForeignKey(
                name: "FK_Overtime_Attendance_SessionID",
                table: "Overtime");

            migrationBuilder.DropForeignKey(
                name: "FK_Overtime_Employee_ApprovedBy",
                table: "Overtime");

            migrationBuilder.DropForeignKey(
                name: "FK_Overtime_Employee_EmployeeID",
                table: "Overtime");

            migrationBuilder.DropForeignKey(
                name: "FK_Overtime_OvertimeRule_OvertimeRuleID",
                table: "Overtime");

            migrationBuilder.DropForeignKey(
                name: "FK_Payroll_Employee_EmployeeID",
                table: "Payroll");

            migrationBuilder.DropForeignKey(
                name: "FK_Payroll_item_BonusType_BonusTypeID",
                table: "Payroll_item");

            migrationBuilder.DropForeignKey(
                name: "FK_Payroll_item_Employee_CreatedBy",
                table: "Payroll_item");

            migrationBuilder.DropForeignKey(
                name: "FK_Payroll_item_Employee_EmployeeID",
                table: "Payroll_item");

            migrationBuilder.DropForeignKey(
                name: "FK_Payroll_item_Payroll_PayID",
                table: "Payroll_item");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Employee_ProjectHead",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_SalaryContract_Employee_CreatedBy",
                table: "SalaryContract");

            migrationBuilder.DropForeignKey(
                name: "FK_SalaryContract_Employee_EmployeeID",
                table: "SalaryContract");

            migrationBuilder.DropForeignKey(
                name: "FK_Termination_Employee_EmployeeID",
                table: "Termination");

            migrationBuilder.DropForeignKey(
                name: "FK_VacationApproval_AspNetRoles_ApprovedByRoleId",
                table: "VacationApproval");

            migrationBuilder.DropForeignKey(
                name: "FK_VacationApproval_Employee_ApprovedByID",
                table: "VacationApproval");

            migrationBuilder.DropForeignKey(
                name: "FK_VacationApproval_VacationRequest_RequestID",
                table: "VacationApproval");

            migrationBuilder.DropForeignKey(
                name: "FK_VacationRequest_Employee_AwaitingApproval",
                table: "VacationRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_VacationRequest_Employee_EmployeeID",
                table: "VacationRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_VacationType_2_Employee_EmployeeID",
                table: "VacationType_2");

            migrationBuilder.AddForeignKey(
                name: "FK_AFKEvent_Attendance_SessionID",
                table: "AFKEvent",
                column: "SessionID",
                principalTable: "Attendance",
                principalColumn: "SessionID");

            migrationBuilder.AddForeignKey(
                name: "FK_AFKEvent_Employee_EmployeeID",
                table: "AFKEvent",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Employee_EmployeeID",
                table: "Attendance",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bonus_BonusType_BonusTypeID",
                table: "Bonus",
                column: "BonusTypeID",
                principalTable: "BonusType",
                principalColumn: "BonusTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bonus_Employee_EmployeeID",
                table: "Bonus",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_AspNetUsers_UserID",
                table: "Employee",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_VacationLevel_Vacation_Level_ID",
                table: "Employee",
                column: "Vacation_Level_ID",
                principalTable: "VacationLevel",
                principalColumn: "Vacation_Level_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDocument_Employee_EmployeeID",
                table: "EmployeeDocument",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProject_Attendance_SessionID",
                table: "EmployeeProject",
                column: "SessionID",
                principalTable: "Attendance",
                principalColumn: "SessionID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProject_Employee_EmployeeID",
                table: "EmployeeProject",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProject_Project_ProjectID",
                table: "EmployeeProject",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_Overtime_Attendance_SessionID",
                table: "Overtime",
                column: "SessionID",
                principalTable: "Attendance",
                principalColumn: "SessionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Overtime_Employee_ApprovedBy",
                table: "Overtime",
                column: "ApprovedBy",
                principalTable: "Employee",
                principalColumn: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Overtime_Employee_EmployeeID",
                table: "Overtime",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Overtime_OvertimeRule_OvertimeRuleID",
                table: "Overtime",
                column: "OvertimeRuleID",
                principalTable: "OvertimeRule",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Payroll_Employee_EmployeeID",
                table: "Payroll",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Payroll_item_BonusType_BonusTypeID",
                table: "Payroll_item",
                column: "BonusTypeID",
                principalTable: "BonusType",
                principalColumn: "BonusTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Payroll_item_Employee_CreatedBy",
                table: "Payroll_item",
                column: "CreatedBy",
                principalTable: "Employee",
                principalColumn: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Payroll_item_Employee_EmployeeID",
                table: "Payroll_item",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Payroll_item_Payroll_PayID",
                table: "Payroll_item",
                column: "PayID",
                principalTable: "Payroll",
                principalColumn: "PayID");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Employee_ProjectHead",
                table: "Project",
                column: "ProjectHead",
                principalTable: "Employee",
                principalColumn: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_SalaryContract_Employee_CreatedBy",
                table: "SalaryContract",
                column: "CreatedBy",
                principalTable: "Employee",
                principalColumn: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_SalaryContract_Employee_EmployeeID",
                table: "SalaryContract",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Termination_Employee_EmployeeID",
                table: "Termination",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_VacationApproval_AspNetRoles_ApprovedByRoleId",
                table: "VacationApproval",
                column: "ApprovedByRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VacationApproval_Employee_ApprovedByID",
                table: "VacationApproval",
                column: "ApprovedByID",
                principalTable: "Employee",
                principalColumn: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_VacationApproval_VacationRequest_RequestID",
                table: "VacationApproval",
                column: "RequestID",
                principalTable: "VacationRequest",
                principalColumn: "RequestID");

            migrationBuilder.AddForeignKey(
                name: "FK_VacationRequest_Employee_AwaitingApproval",
                table: "VacationRequest",
                column: "AwaitingApproval",
                principalTable: "Employee",
                principalColumn: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_VacationRequest_Employee_EmployeeID",
                table: "VacationRequest",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_VacationType_2_Employee_EmployeeID",
                table: "VacationType_2",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "EmployeeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AFKEvent_Attendance_SessionID",
                table: "AFKEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_AFKEvent_Employee_EmployeeID",
                table: "AFKEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Employee_EmployeeID",
                table: "Attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_Bonus_BonusType_BonusTypeID",
                table: "Bonus");

            migrationBuilder.DropForeignKey(
                name: "FK_Bonus_Employee_EmployeeID",
                table: "Bonus");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_AspNetUsers_UserID",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_VacationLevel_Vacation_Level_ID",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDocument_Employee_EmployeeID",
                table: "EmployeeDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProject_Attendance_SessionID",
                table: "EmployeeProject");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProject_Employee_EmployeeID",
                table: "EmployeeProject");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProject_Project_ProjectID",
                table: "EmployeeProject");

            migrationBuilder.DropForeignKey(
                name: "FK_Overtime_Attendance_SessionID",
                table: "Overtime");

            migrationBuilder.DropForeignKey(
                name: "FK_Overtime_Employee_ApprovedBy",
                table: "Overtime");

            migrationBuilder.DropForeignKey(
                name: "FK_Overtime_Employee_EmployeeID",
                table: "Overtime");

            migrationBuilder.DropForeignKey(
                name: "FK_Overtime_OvertimeRule_OvertimeRuleID",
                table: "Overtime");

            migrationBuilder.DropForeignKey(
                name: "FK_Payroll_Employee_EmployeeID",
                table: "Payroll");

            migrationBuilder.DropForeignKey(
                name: "FK_Payroll_item_BonusType_BonusTypeID",
                table: "Payroll_item");

            migrationBuilder.DropForeignKey(
                name: "FK_Payroll_item_Employee_CreatedBy",
                table: "Payroll_item");

            migrationBuilder.DropForeignKey(
                name: "FK_Payroll_item_Employee_EmployeeID",
                table: "Payroll_item");

            migrationBuilder.DropForeignKey(
                name: "FK_Payroll_item_Payroll_PayID",
                table: "Payroll_item");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Employee_ProjectHead",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_SalaryContract_Employee_CreatedBy",
                table: "SalaryContract");

            migrationBuilder.DropForeignKey(
                name: "FK_SalaryContract_Employee_EmployeeID",
                table: "SalaryContract");

            migrationBuilder.DropForeignKey(
                name: "FK_Termination_Employee_EmployeeID",
                table: "Termination");

            migrationBuilder.DropForeignKey(
                name: "FK_VacationApproval_AspNetRoles_ApprovedByRoleId",
                table: "VacationApproval");

            migrationBuilder.DropForeignKey(
                name: "FK_VacationApproval_Employee_ApprovedByID",
                table: "VacationApproval");

            migrationBuilder.DropForeignKey(
                name: "FK_VacationApproval_VacationRequest_RequestID",
                table: "VacationApproval");

            migrationBuilder.DropForeignKey(
                name: "FK_VacationRequest_Employee_AwaitingApproval",
                table: "VacationRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_VacationRequest_Employee_EmployeeID",
                table: "VacationRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_VacationType_2_Employee_EmployeeID",
                table: "VacationType_2");

            migrationBuilder.AddForeignKey(
                name: "FK_AFKEvent_Attendance_SessionID",
                table: "AFKEvent",
                column: "SessionID",
                principalTable: "Attendance",
                principalColumn: "SessionID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_AFKEvent_Employee_EmployeeID",
                table: "AFKEvent",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Employee_EmployeeID",
                table: "Attendance",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Bonus_BonusType_BonusTypeID",
                table: "Bonus",
                column: "BonusTypeID",
                principalTable: "BonusType",
                principalColumn: "BonusTypeID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Bonus_Employee_EmployeeID",
                table: "Bonus",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_AspNetUsers_UserID",
                table: "Employee",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_VacationLevel_Vacation_Level_ID",
                table: "Employee",
                column: "Vacation_Level_ID",
                principalTable: "VacationLevel",
                principalColumn: "Vacation_Level_ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDocument_Employee_EmployeeID",
                table: "EmployeeDocument",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProject_Attendance_SessionID",
                table: "EmployeeProject",
                column: "SessionID",
                principalTable: "Attendance",
                principalColumn: "SessionID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProject_Employee_EmployeeID",
                table: "EmployeeProject",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProject_Project_ProjectID",
                table: "EmployeeProject",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "ProjectID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Overtime_Attendance_SessionID",
                table: "Overtime",
                column: "SessionID",
                principalTable: "Attendance",
                principalColumn: "SessionID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Overtime_Employee_ApprovedBy",
                table: "Overtime",
                column: "ApprovedBy",
                principalTable: "Employee",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Overtime_Employee_EmployeeID",
                table: "Overtime",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Overtime_OvertimeRule_OvertimeRuleID",
                table: "Overtime",
                column: "OvertimeRuleID",
                principalTable: "OvertimeRule",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Payroll_Employee_EmployeeID",
                table: "Payroll",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Payroll_item_BonusType_BonusTypeID",
                table: "Payroll_item",
                column: "BonusTypeID",
                principalTable: "BonusType",
                principalColumn: "BonusTypeID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Payroll_item_Employee_CreatedBy",
                table: "Payroll_item",
                column: "CreatedBy",
                principalTable: "Employee",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Payroll_item_Employee_EmployeeID",
                table: "Payroll_item",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Payroll_item_Payroll_PayID",
                table: "Payroll_item",
                column: "PayID",
                principalTable: "Payroll",
                principalColumn: "PayID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Employee_ProjectHead",
                table: "Project",
                column: "ProjectHead",
                principalTable: "Employee",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_SalaryContract_Employee_CreatedBy",
                table: "SalaryContract",
                column: "CreatedBy",
                principalTable: "Employee",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_SalaryContract_Employee_EmployeeID",
                table: "SalaryContract",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Termination_Employee_EmployeeID",
                table: "Termination",
                column: "EmployeeID",
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

            migrationBuilder.AddForeignKey(
                name: "FK_VacationApproval_Employee_ApprovedByID",
                table: "VacationApproval",
                column: "ApprovedByID",
                principalTable: "Employee",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_VacationApproval_VacationRequest_RequestID",
                table: "VacationApproval",
                column: "RequestID",
                principalTable: "VacationRequest",
                principalColumn: "RequestID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_VacationRequest_Employee_AwaitingApproval",
                table: "VacationRequest",
                column: "AwaitingApproval",
                principalTable: "Employee",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_VacationRequest_Employee_EmployeeID",
                table: "VacationRequest",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_VacationType_2_Employee_EmployeeID",
                table: "VacationType_2",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
