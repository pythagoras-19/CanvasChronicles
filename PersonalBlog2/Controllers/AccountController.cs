using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PersonalBlog2.Models;

namespace PersonalBlog2.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUserModel> _userManager;
    private readonly SignInManager<ApplicationUserModel> _signInManager;
    public AccountController(UserManager<ApplicationUserModel> userManager, SignInManager<ApplicationUserModel> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    // GET: Account/Login
    public IActionResult Login()
    {
        return View();
    }

    // POST: Account/Login
    [HttpPost]
    public async Task <IActionResult> Login(string username, string password)
    {
        var result = 
            await _signInManager.PasswordSignInAsync(username, password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            // If successful, redirect to the home page or a return URL if you have one
            return RedirectToAction("Index", "Home");
        }
        else
        {
            // login fail
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();
        }
    }
}