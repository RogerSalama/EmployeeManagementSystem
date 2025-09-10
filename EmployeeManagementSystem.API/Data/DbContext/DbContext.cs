using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Entities;
using Microsoft.EntityFrameworkCore.Design;

namespace EmployeeManagementSystem.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Business tables
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<AFKEvent> AFKEvent { get; set; }
        public DbSet<Overtime> Overtime { get; set; }
        public DbSet<OvertimeRule> OvertimeRule { get; set; }
        public DbSet<Bonus> Bonus { get; set; }
        public DbSet<BonusType> BonusType { get; set; }
        public DbSet<HolidayCalendar> HolidayCalendar { get; set; }
        public DbSet<VacationLevel> VacationLevel { get; set; }
        public DbSet<Termination> Termination { get; set; }
        public DbSet<Payroll> Payroll { get; set; }
        public DbSet<Payroll_Item> Payroll_item { get; set; }
        public DbSet<Employee_Project> EmployeeProject { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<EmployeeDocument> EmployeeDocument { get; set; }
        public DbSet<SalaryContract> SalaryContract { get; set; }
        public DbSet<VacationApproval> VacationApproval { get; set; }
        public DbSet<VacationType_2> VacationType_2 { get; set; }
        public DbSet<VacationRequest> VacationRequest { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Apply "NoAction" as the default for all relationships
            foreach (var foreignKey in builder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.NoAction;
            }
        }
    }
}
