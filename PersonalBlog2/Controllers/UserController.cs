using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PersonalBlog2.Data;
using PersonalBlog2.DataAccess;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace PersonalBlog2.Controllers;

public class UserController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly DataQueryTest _dataQueryTest;
    private readonly IConfiguration _configuration;
    
    public UserController(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        // retrieve it from configuration
        _configuration = configuration; // Set the configuration
        
        // Access the connection string from configuration
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        
        _dataQueryTest = new DataQueryTest(connectionString: connectionString);
    }
    

    public async Task<IActionResult> TestDatabaseConnection()
    {
        try
        {
            var users = await _context.Users.ToListAsync();
            return Json(users); 
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }
    
    public ActionResult Index()
    {
        var users = _dataQueryTest.GetAllUsers();
        return Json(users);
    }
}