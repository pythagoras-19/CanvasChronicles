using Microsoft.EntityFrameworkCore;
using PersonalBlog2.Data;
using PersonalBlog2.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddHttpClient();

// Retrieve the base connection string
var baseConnectionString = builder.Configuration.GetConnectionString("CanvasChroniclesDb");

// Retrieve the password environment variable
var dbPassword = Environment.GetEnvironmentVariable("CanvasChroniclesDBPassword");

// Append the password to the connection string
var connectionString = $"{baseConnectionString}password={dbPassword};";

// Use the complete connection string for the DbContext configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services
    .AddDefaultIdentity<ApplicationUserModel>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Register MVC
builder.Services.AddControllersWithViews();

// Register background service 
/*
 * TODO: Eliminate the ArtGenerationService from the application
 */
// builder.Services.AddHostedService<ArtGenerationService>(); 

var app = builder.Build();

// Test db connection
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        if (context.Database.CanConnect())
        {
            Console.WriteLine("Successfully connected to the database.");
        }
        else
        {
            Console.WriteLine("Failed to connect to the database.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while connecting to the database: {ex.Message}");
    }
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();