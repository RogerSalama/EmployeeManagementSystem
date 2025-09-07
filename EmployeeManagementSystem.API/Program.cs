using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Infrastructure.Data;
using EmployeeManagementSystem.Entities;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using EmployeeManagementSystem.Desktop.Models;
//using EmployeeManagementSystem.Data; // Ensure this namespace is added for AppDbContext
using System;

var builder = WebApplication.CreateBuilder(args);

// EF Core + Identity
builder.Services.AddDbContext<DbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Fix for CS1061: Use AddEntityFrameworkStores instead of AddEntityFrameworkStore
//hena ana salaht error kan maktoob badal ApplicationUser ( ely hwa class gowa file account ely fe entities ) Account we makansh 2areeha  
builder.Services.AddIdentity<Account, IdentityRole>(options =>
{
    // Password stuff
    options.Password.RequireDigit = true;            // must have a number
    options.Password.RequiredLength = 8;             // min 8 chars
    options.Password.RequireNonAlphanumeric = true;  // must have special characters
    options.Password.RequireUppercase = true;        // min one uppercase letter
    options.Password.RequireLowercase = true;        // min one lowercase letter
    options.Password.RequiredUniqueChars = 3;        // min 3 distinct characters

    // must have a unique email for the user
    options.User.RequireUniqueEmail = true;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Lockout duration
    options.Lockout.MaxFailedAccessAttempts = 5;                      // Max failed attempts
    options.Lockout.AllowedForNewUsers = true;                        // Apply lockout to new users too
})
.AddEntityFrameworkStores<DbContext>() 
.AddDefaultTokenProviders();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();