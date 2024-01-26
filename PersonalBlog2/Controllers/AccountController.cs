// TODO: Maybe need to get rid of me!
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
            return RedirectToAction("Index", "Home");
        }
        else
        {
            // login fail
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();
        }
    }
    
    // Register
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUserModel { UserName = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                //  TODO: Log the user in or redirect to a different page
                //  TODO: return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
        }

        // If we got this far, something failed, redisplay form
        return View(model);
    }

}