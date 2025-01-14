using BlogApp.Application.Services;
using BlogApp.Application.Users.Commands.CreateUser;
using BlogApp.Core.Entities;
using BlogApp.Core.Interfaces;
using BlogApp.Infrastructure.DbContexts;
using BlogApp.Infrastructure.Repositories;
using BlogApp.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
});

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/myapp.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddLogging(logging =>
{
    logging.AddSerilog();
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("blogDb")));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("EditorPolicy", policy => policy.RequireRole("Editor"));
});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRepository<BlogPost>, BlogPostRepository>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Register MediatR services (single registration)
//builder.Services.AddValidatorsFromAssembly(typeof(CreateUserCommandValidator).Assembly);

// Register MediatR with validation behavior
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateUserCommandValidator).Assembly);
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Account/AccessDenied"; 
});

// Build the application
var app = builder.Build();

// Seed roles and admin user
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    Console.WriteLine("Seeding roles and admin user...");
    await SeedData.InitializeAsync(services);
}

// Configure Middleware
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Map routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

await app.RunAsync();
