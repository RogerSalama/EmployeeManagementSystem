using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

public static class IdentitySeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        // 1. Define roles
        string[] roles = { "Admin", "Manager", "Employee","Head of Unit", "Accountant" };

        // 2. Create roles if they don't exist
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // 3. Create a default Admin user
        var adminEmail = "admin@example.com";
        var admin = await userManager.FindByEmailAsync(adminEmail);
        if (admin == null)
        {
            admin = new ApplicationUser
            {
                UserName = "admin",
                Email = adminEmail,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(admin, "SecurePass123!");
            await userManager.AddToRoleAsync(admin, "Admin");
        }
        //creating a default employee... (omar)
        var empEmail = "omar@gmail.com";
        var employee = await userManager.FindByEmailAsync(empEmail);
        if (employee == null)
        {
            employee = new ApplicationUser
            {
                UserName = "Omar",
                Email = empEmail,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(employee, "SecurePass123!");
            await userManager.AddToRoleAsync(employee, "Employee");
        }
        //creating a default Manager
        var manageremail = "manager@gmail.com";
        var manager = await userManager.FindByEmailAsync(manageremail);
        if (manager == null)
        {
            manager = new ApplicationUser
            {
                UserName = "Besheer",
                Email = manageremail,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(manager, "SecurePass123!");
            await userManager.AddToRoleAsync(manager, "Manager");
        }
        //creating a default head of unit
        var houemail = "hou@gmail.com";
        var hou = await userManager.FindByEmailAsync(houemail);
        if (hou == null)
        {
            hou = new ApplicationUser
            {
                UserName = "Roger",
                Email = houemail,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(hou, "SecurePass123!");
            await userManager.AddToRoleAsync(hou, "Head of Unit");
        }

        // 4. Add permissions as claims (example)
        var adminRole = await roleManager.FindByNameAsync("Admin");
        var adminClaims = await roleManager.GetClaimsAsync(adminRole);
        if (!adminClaims.Any(c => c.Type == "Permission" && c.Value == "Employee.Create"))
        {
            await roleManager.AddClaimAsync(adminRole, new Claim("Permission", "Employee.Create"));
            await roleManager.AddClaimAsync(adminRole, new Claim("Permission", "Employee.Delete"));
            await roleManager.AddClaimAsync(adminRole, new Claim("Permission", "User.Manage"));
        }

        var managerRole = await roleManager.FindByNameAsync("Manager");
        var managerClaims = await roleManager.GetClaimsAsync(managerRole);
        if (!managerClaims.Any(c => c.Type == "Permission" && c.Value == "Employee.View"))
        {
            await roleManager.AddClaimAsync(managerRole, new Claim("Permission", "Employee.View"));
        }

        var employeeRole = await roleManager.FindByNameAsync("Employee");
        var employeeClaims = await roleManager.GetClaimsAsync(employeeRole);
        if (!employeeClaims.Any(c => c.Type == "Permission" && c.Value == "Self.ViewProfile"))
        {
            await roleManager.AddClaimAsync(employeeRole, new Claim("Permission", "Self.ViewProfile"));
        }

        var accountantRole = await roleManager.FindByNameAsync("Accountant");
        var accountantClaims = await roleManager.GetClaimsAsync(accountantRole);
        if (!accountantClaims.Any(c => c.Type == "Permission" && c.Value == "Finance.View"))
        {
            await roleManager.AddClaimAsync(accountantRole, new Claim("Permission", "Finance.View"));
            await roleManager.AddClaimAsync(accountantRole, new Claim("Permission", "Finance.Update"));
        }
    }
}
