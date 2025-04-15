using FrostFlow.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FrostFlow.Controllers
{
  public class AccountController : Controller
  {
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
      _signInManager = signInManager;
      _userManager = userManager;
    }

    // GET: /Account/Login
    [HttpGet]
    public IActionResult Login()
    {
      return View("Login"); // Use custom view in Views/Account/Login.cshtml
    }

    // POST: /Account/Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(User model, string returnUrl = null)
    {
      if (ModelState.IsValid)
      {
        var result = await _signInManager.PasswordSignInAsync(
            model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

        if (result.Succeeded)
        {
          return RedirectToLocal(returnUrl);
        }
        else
        {
          ModelState.AddModelError("", "Invalid login attempt.");
        }
      }

      return View("Login", model); // Use custom view in Views/Account/Login.cshtml
    }

    // GET: /Account/Register
    [HttpGet]
    public IActionResult Register()
    {
      return View("Register"); // Use custom view in Views/Account/Register.cshtml
    }

    // POST: /Account/Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
      if (ModelState.IsValid)
      {
        var user = new IdentityUser { UserName = model.UserName, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
          // Sign in the user automatically after registration
          await _signInManager.SignInAsync(user, isPersistent: false);
          return RedirectToAction("Index", "Home"); // Redirect to Home after successful registration
        }

        // Add error messages to the model if the registration fails
        foreach (var error in result.Errors)
        {
          ModelState.AddModelError("", error.Description);
        }
      }

      return View("Register", model); // Use custom view in Views/Account/Register.cshtml
    }

    // POST: /Account/Logout
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Index", "Home"); // Redirect to Home after logout
    }

    private IActionResult RedirectToLocal(string returnUrl)
    {
      return Url.IsLocalUrl(returnUrl) ? Redirect(returnUrl) : RedirectToAction("Index", "Home");
    }
  }
}
