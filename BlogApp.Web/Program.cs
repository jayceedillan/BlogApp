using BlogApp.Application.Services;
using BlogApp.Application.Users.Commands.CreateUser;
using BlogApp.Core.Entities;
using BlogApp.Core.Interfaces;
using BlogApp.Infrastructure.DbContexts;
using BlogApp.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Logging
builder.Services.AddLogging(logging =>
{
    logging.AddConsole();  // Enables console logging
    logging.AddDebug();    // Enables debug logging
});

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/myapp.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddLogging(logging =>
{
    logging.AddSerilog(); // Adds Serilog to the logging pipeline
});


// Configure Services
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("blogDb")));

// Make sure to use ApplicationUser for Identity setup
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    //options.Password.RequireDigit = true; 
    //options.Password.RequireLowercase = true; 
    //options.Password.RequireUppercase = true; 
    //options.Password.RequireNonAlphanumeric = true; 
    //options.Password.RequiredLength = 8; 
})
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
//builder.Services.AddValidatorsFromAssemblyContaining<CreateUserDtoValidator>();
// Add FluentValidation for model validation
//builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Register MediatR services (single registration)
builder.Services.AddValidatorsFromAssembly(typeof(CreateUserCommandValidator).Assembly);

// Register MediatR with validation behavior
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly);
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Account/AccessDenied"; // Redirects here when access is denied
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
