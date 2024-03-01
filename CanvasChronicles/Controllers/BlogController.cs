using CanvasChronicles.Data;
using CanvasChronicles.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

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
    
   [HttpGet]
    public IActionResult Create()
    {
        _logger.LogInformation("Create blog post page visited.");
        // return the view for the form to create a new blog post.
        // prepare any necessary data for the form here before returning
        return View();
    }

    public async Task<IActionResult> Index()
    {
        _logger.LogInformation("Blog index page visited.");
        try
        {
            var blogPosts = await _context.BlogPosts.OrderByDescending(p => p.Created).ToListAsync();
            _logger.LogInformation("Blog posts retrieved from database.");
            return View(blogPosts);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving blog posts from the database.");
            ViewBag.ErrorMessage = "An error occurred while retrieving blog posts from the database.Please try again later or contact support.";
           // Console.WriteLine(e);
           // throw;
           return View(new List<BlogPost>());
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(BlogPost blogPost)
    {
        ModelState.Remove("Author");
        _logger.LogInformation("Create blog post form called.");
        blogPost.Author = User.Identity.Name ?? "Anonymous";
        _logger.LogInformation($"User {blogPost.Author} is creating a blog post.");
        if (ModelState.IsValid)
        {
            if (blogPost.Author == "Anonymous")
            {
                _logger.LogInformation("Could not get the correct author.\nAnonymous user created a blog post.");
            }
            else
            {
                _logger.LogInformation($"User {blogPost.Author} created a blog post.");
            }

            // Set the created and updated dates to now
            blogPost.Created = DateTime.UtcNow;
            blogPost.Updated = DateTime.UtcNow;
            
            // log the data to be put in the database
            _logger.LogInformation($"Title: {blogPost.Title}\n" +
                                   $"Content: " +
                                   $"{blogPost.Content}\n" +
                                   $"Created: {blogPost.Created}\n" +
                                   $"Updated: {blogPost.Updated}\n" +
                                   $"Author: {blogPost.Author}");

            _context.BlogPosts.Add(blogPost);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Blog post saved to database.");
            return RedirectToAction(nameof(Index)); 
        }
        else
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                _logger.LogError($"Validation Error: {error.ErrorMessage}");
            }
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