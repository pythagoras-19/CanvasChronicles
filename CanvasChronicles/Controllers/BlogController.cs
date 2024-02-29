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
    
    public IActionResult Create()
    {
        // return the view for the form to create a new blog post.
        //  prepare any necessary data for the form here before returning
        return View();
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
            _context.BlogPosts.Add(blogPost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); 
        }
        return View(blogPost);
    }

    // TEST ONLY: This method is for demonstration and should NOT be exposed in production without proper modifications.
   [HttpGet]
    public async Task<IActionResult> AddSampleBlogPost()
    {
        _logger.LogInformation("Adding sample blog post.");
        var newBlogPost = new BlogPost
        {
            Title = "Sample Blog Post",
            Content = "This is a sample blog post.",
            Created = DateTime.Now,
            Updated = DateTime.Now,
            Author = "Sample Author"
        };

        _context.BlogPosts.Add(newBlogPost);
        await _context.SaveChangesAsync();
        
        _logger.LogInformation("Sample blog post added.");

        return RedirectToAction(nameof(Index));
    }
}