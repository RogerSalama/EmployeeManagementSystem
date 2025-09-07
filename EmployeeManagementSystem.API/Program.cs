using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Infrastructure.Data; // <-- adjust if ApplicationDbContext is elsewhere

var builder = WebApplication.CreateBuilder(args);

// 1. Add DbContext (make sure "DefaultConnection" exists in appsettings.json)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Add Identity (users + roles)
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// 3. Add Authentication & Authorization
builder.Services.AddAuthentication();
builder.Services.AddAuthorization(options =>
{
    // Example: map permission claims into policies
    options.AddPolicy("Employee.Create", policy =>
        policy.RequireClaim("Permission", "Employee.Create"));

    options.AddPolicy("Employee.Delete", policy =>
        policy.RequireClaim("Permission", "Employee.Delete"));

    options.AddPolicy("User.Manage", policy =>
        policy.RequireClaim("Permission", "User.Manage"));

    options.AddPolicy("Employee.View", policy =>
        policy.RequireClaim("Permission", "Employee.View"));

    options.AddPolicy("Self.ViewProfile", policy =>
        policy.RequireClaim("Permission", "Self.ViewProfile"));

    options.AddPolicy("Finance.View", policy =>
        policy.RequireClaim("Permission", "Finance.View"));

    options.AddPolicy("Finance.Update", policy =>
        policy.RequireClaim("Permission", "Finance.Update"));
});

// 4. Add controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 5. Seed roles, users, and claims
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await IdentitySeeder.SeedAsync(services);
}

// 6. Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Authentication & Authorization middleware
app.UseAuthentication();  // 👈 must be before UseAuthorization
app.UseAuthorization();

app.MapControllers();

app.Run();
