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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// 1. Add DbContext (make sure "DefaultConnection" exists in appsettings.json)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Add Identity (users + roles)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

//omar added this to allow jwt to store employeeId, needed a new package for JwtBearer !!!!!!!!!!!!!!!!!!!!!
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        NameClaimType = "EmployeeId", 
        RoleClaimType = ClaimTypes.Role
    };
});
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
builder.Services.AddScoped<TokenGeneration>();    
builder.Services.AddScoped<DBCheckin>();
builder.Services.AddScoped<VacationService>();


var app = builder.Build();

app.Use(async (context, next) =>
{
    if (context.Request.Headers.ContainsKey("Authorization"))
        Console.WriteLine("Auth header: " + context.Request.Headers["Authorization"]);
    else
        Console.WriteLine("No auth header found!");

    await next();
});

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();