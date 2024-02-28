using Microsoft.AspNetCore.Mvc;

namespace CanvasChronicles.Controllers; 

public class BlogController : Controller
{
    private readonly ILogger<BlogController> _logger;
    
    public BlogController(ILogger<BlogController> logger)
    {
        _logger = logger;
    }
    
    public IActionResult Index()
    {
        _logger.LogInformation("Blog index page visited.");
        return View();
    }
}