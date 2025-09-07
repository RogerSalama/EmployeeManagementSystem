using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

public static class IdentitySeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

        // 1. Define roles
        string[] roles = { "Admin", "Manager", "Employee", "Accountant" };

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
            admin = new IdentityUser
            {
                UserName = "admin",
                Email = adminEmail,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(admin, "SecurePass123!"); // ⚠️ change before production
            await userManager.AddToRoleAsync(admin, "Admin");
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
