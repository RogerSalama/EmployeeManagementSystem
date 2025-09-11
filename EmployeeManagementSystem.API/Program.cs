using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.API.Data;
using EmployeeManagementSystem.Entities;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using EmployeeManagementSystem.Desktop.Models;
//using EmployeeManagementSystem.Data; // Ensure this namespace is added for AppDbContext
using System;
using EmployeeManagementSystem.API.Services;
using NSwag;
using NSwag.Generation.Processors.Security;

var builder = WebApplication.CreateBuilder(args);


// Just a background service for the Punching System
/*builder.Services.AddHostedService<PunchService>();
*/

// 1. Add DbContext (make sure "DefaultConnection" exists in appsettings.json)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Add Identity (users + roles)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
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

builder.Services.AddOpenApiDocument(options =>
{
    options.AddSecurity("Bearer", new OpenApiSecurityScheme
    {
        Description = "Bearer token authorization header",
        Type = OpenApiSecuritySchemeType.Http,
        In = OpenApiSecurityApiKeyLocation.Header,
        Name = "Authorization",
        Scheme = "Bearer"
    });

    options.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("Bearer"));
});


builder.Services.AddHttpClient<timeStamp>();
builder.Services.AddScoped<PunchService>();
builder.Services.AddScoped<LockoutService>();
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
    app.UseOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

// Authentication & Authorization middleware
app.UseAuthentication();  // 👈 must be before UseAuthorization
app.UseAuthorization();

app.MapControllers();

app.Run();