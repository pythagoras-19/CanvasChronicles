using CanvasChronicles.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace CanvasChronicles.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    public AccountController(
        UserManager<User> userManager, 
        SignInManager<User> signInManager,
        ILogger<AccountController> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
        _logger.LogInformation("AccountController created.");
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
            return RedirectToAction("Index", "Home");
        }
        else
        {
            // login fail
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();
        }
    }
    // GET: Account/Register
    public IActionResult Register()
    {
        _logger.LogInformation("Register page accessed."); // Log access to register page
        return View();
    }
    // POST: Account/Register
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new User { UserName = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation($"New user registered: {model.Email}");
                // TODO: Log the user in or redirect to a different page
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    _logger.LogWarning($"Registration failed: {error.Description}");
                }
            }
        }

        // If we got this far, something failed, redisplay form
        _logger.LogWarning("Registration failed: Invalid model state.");
        return View(model);
    }
}