using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FrostFlow.Models;
using System.Threading.Tasks;

namespace FrostFlow.Controllers
{
  public class AccountController : Controller
  {
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
      _userManager = userManager;
      _signInManager = signInManager;
    }

    public IActionResult Login()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
      var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
      if (result.Succeeded)
      {
        return RedirectToAction("Index", "Home");
      }

      ViewBag.ErrorMessage = "Invalid username or password";
      return View();
    }

    public IActionResult Register()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(string username, string email, string password)
    {
      var user = new User { UserName = username, Email = email };
      var result = await _userManager.CreateAsync(user, password);

      if (result.Succeeded)
      {
        await _signInManager.SignInAsync(user, false);
        return RedirectToAction("Index", "Home");
      }

      ViewBag.ErrorMessage = "Registration failed";
      return View();
    }

    public async Task<IActionResult> Logout()
    {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Login", "Account");
    }
  }
}
