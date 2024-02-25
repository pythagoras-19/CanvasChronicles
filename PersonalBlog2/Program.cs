using Microsoft.EntityFrameworkCore;
using PersonalBlog2.Data;
using PersonalBlog2.DataAccess;
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
var tester = new DatabaseConnectionTest(connectionString);
bool isConnected = tester.IsDatabaseConnected();
Console.WriteLine(isConnected ? "Connected to the database." : "Failed to connect to the database.");

// Create a scope to get scoped services
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var dbContext = services.GetRequiredService<ApplicationDbContext>();
        var posts = dbContext.Users.ToList(); // Assuming you have a Users DbSet and want to list Users not Posts
        Console.WriteLine($"There are {posts.Count} users in the database.");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while querying the database.");
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