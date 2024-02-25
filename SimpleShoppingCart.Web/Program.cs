using SimpleShoppingCart.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SimpleShoppingCart.DataAccess.Models;
using SimpleShoppingCart.DataAccess.Repositories;
using SimpleShoppingCart.DataAccess.Repositories.Interfaces;
using SimpleShoppingCart.BusinessLogic.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = Environment.GetEnvironmentVariable("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString, x => x.MigrationsAssembly("SimpleShoppingCart.DataAccess")));

if (builder.Environment.IsDevelopment())
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Business layer services
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<ITerminalService, TerminalService>();

builder.Services.AddControllersWithViews();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

    var applicationDbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

    applicationDbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
