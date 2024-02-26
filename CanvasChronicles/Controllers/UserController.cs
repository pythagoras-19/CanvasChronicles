using CanvasChronicles.Data;
using CanvasChronicles.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CanvasChronicles.Controllers;

public class UserController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly DataQueryTest _dataQueryTest;
    private readonly IConfiguration _configuration;
    
    public UserController(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
        
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
    
    public async Task<IActionResult> TestQuery()
    {
        var users = await _context.Users.ToListAsync();
        return Json(users); 
    }
}