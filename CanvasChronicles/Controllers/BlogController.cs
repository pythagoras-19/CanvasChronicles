using CanvasChronicles.Data;
using CanvasChronicles.Models;
using Microsoft.AspNetCore.Mvc;

namespace CanvasChronicles.Controllers; 

public class BlogController : Controller
{
    private readonly ILogger<BlogController> _logger;
    private readonly BloggingContext _context;
    
    public BlogController(ILogger<BlogController> logger, BloggingContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    public IActionResult Index()
    {
        _logger.LogInformation("Blog index page visited.");
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(BlogPost blogPost)
    {
        if (ModelState.IsValid)
        {
            _context.Add(blogPost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); 
        }
        return View(blogPost);
    }
}