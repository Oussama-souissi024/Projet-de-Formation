using E_Commerce.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using E_Commerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Application.Common.Extension;
using E_Commerce.Infrastructure.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext with explicit SQL Server configuration
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()));

// Add Identity after DbContext
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Add custom service registrations
builder.Services.AddInfrastructureRegistration(builder.Configuration);
builder.Services.AddServiceRegistration();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddLogging(); // Ensures logging services are available

// Configure logging to the console
builder.Logging.AddConsole();

// Register HttpClient 
builder.Services.AddHttpClient();

Stripe.StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();