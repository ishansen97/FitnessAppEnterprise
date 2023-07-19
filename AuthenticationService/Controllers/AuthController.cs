using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticationService.Data.Models;
using AuthenticationService.Model;
using IdentityModel;
using IdentityServer4.Services;
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
    private readonly IIdentityServerInteractionService _interactionService;

    public AuthController(
      ILogger<AuthController> logger,
      UserManager<AppUser> userManager,
      SignInManager<AppUser> signInManager, 
      IIdentityServerInteractionService interactionService)
    {
      _logger = logger;
      _userManager = userManager;
      _signInManager = signInManager;
      _interactionService = interactionService;
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

    [HttpGet]
    public async Task<IActionResult> Logout(string logoutId)
    {
      await _signInManager.SignOutAsync();
      var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);
      _logger.LogInformation($"Log out Id: {logoutId}");
      _logger.LogInformation($"post logout redirect url: {logoutRequest.PostLogoutRedirectUri}");
      if (string.IsNullOrEmpty(logoutRequest.PostLogoutRedirectUri))
      {
        return RedirectToAction("Login");
      }

      return Redirect(logoutRequest.PostLogoutRedirectUri);
    }

    [HttpGet]
    public async Task<IActionResult> Register()
    {
      return View(nameof(Login));
    }

    [HttpPost]
    public async Task<IActionResult> Register(LoginModel model)
    {
      return View(nameof(Login));
    }
  }
}
