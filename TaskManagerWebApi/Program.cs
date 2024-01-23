using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagerWebApi;
using TaskManagerWebApi.DataAccess;
using TaskManagerWebApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// controller
builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
});

// swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// efCore
builder.Services.AddDbContext<TaskManagerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DbConn"]);
});

// project services
builder.Services.ConfigureAppServices(builder.Configuration);

// automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = true;
})
 .AddEntityFrameworkStores<TaskManagerDbContext>()
 .AddDefaultTokenProviders()
 .AddUserStore<UserStore<ApplicationUser, ApplicationRole, TaskManagerDbContext, int>>()
 .AddRoleStore<RoleStore<ApplicationRole, TaskManagerDbContext, int>>();

// jwt authentication
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
        };
    });

builder.Services.AddAuthorization();








//----------------------------------------------

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHsts();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(options =>
{
    options.WithOrigins("http://localhost:4200")
        .WithHeaders("Authorization", "origin", "accept", "content-type")
        .WithMethods("GET", "POST", "PUT", "DELETE");
});

app.UseAuthentication();

app.UseAuthorization();

//create default user and roles -- comment after first compile
using (var scope = app.Services.CreateScope())
{
    var _userManger = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
    var _roleManger = scope.ServiceProvider.GetService<RoleManager<ApplicationRole>>();
    var _signInManger = scope.ServiceProvider.GetService<SignInManager<ApplicationRole>>();

    if (!await _roleManger.RoleExistsAsync("Admin"))
    {
        var applicationRole = new ApplicationRole()
        {
            Name = "Admin"
        };
        await _roleManger.CreateAsync(applicationRole);
    }
    if (!await _roleManger.RoleExistsAsync("Employee"))
    {
        var applicationRole = new ApplicationRole()
        {
            Name = "Employee"
        };
        await _roleManger.CreateAsync(applicationRole);
    }

    var user = await _userManger.FindByNameAsync("admin");
    if (user == null)
    {
        var appUser = new ApplicationUser()
        {
            UserName = "admin",
            Email = "admin@gmail.com"
        };
        string password = "Admin123#";

        await _userManger.CreateAsync(appUser, password);
    }
}

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
