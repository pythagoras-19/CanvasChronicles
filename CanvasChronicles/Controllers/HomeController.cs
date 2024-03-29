﻿using System.Diagnostics;
using CanvasChronicles.Models;
using Microsoft.AspNetCore.Mvc;

namespace CanvasChronicles.Controllers;

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
    
    // TODO: FIX ME
    [HttpPost]
    public IActionResult CreatePost(Post post)
    {
        if (ModelState.IsValid)
        {
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