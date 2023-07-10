using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticationService.Data.Models;
using AuthenticationService.Model;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AuthenticationService.Controllers
{
  public class AuthController : Controller
  {
    private readonly ILogger<AuthController> _logger;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AuthController(
      ILogger<AuthController> logger,
      UserManager<AppUser> userManager,
      SignInManager<AppUser> signInManager)
    {
      _logger = logger;
      _userManager = userManager;
      _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
      _logger.LogInformation($"return url: {returnUrl}");
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
      _logger.LogInformation($"username: {model.UserName}, password: {model.Password}");
      _logger.LogInformation($"return url: {model.ReturnUrl}");

      var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
      if (result.Succeeded)
      {
        return Redirect(model.ReturnUrl);
      }
      return View();
    }
  }
}
