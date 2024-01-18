using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog2.Models;

namespace PersonalBlog2.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult CreatePost()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult CreatePost(Post post)
    {
        if (ModelState.IsValid)
        {
            // TODO: Save post to database
            _logger.LogInformation("Title: {Title}", post.Title);
            _logger.LogInformation("Content: {Content}", post.Content);
            
            return RedirectToAction("Index");
        }
        
        return View(post);
    }
    
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}