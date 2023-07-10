using FitnessAppEnterprise.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace FitnessAppEnterprise.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpClientFactory _clientFactory;

    public HomeController(
      ILogger<HomeController> logger,
      IHttpClientFactory clientFactory)
    {
      _logger = logger;
      _clientFactory = clientFactory;
    }

    [Authorize]
    public IActionResult Index()
    {
      var userClaims = User.Claims.ToList();
      string userName = string.Empty;
      foreach (var userClaim in userClaims)
      {
        if (userClaim.Type == "name")
        {
          userName = userClaim.Value;
        }
      }
      ViewData["userName"] = userName;
      return View();
    }

    [Authorize]
    public async Task<IActionResult> Privacy()
    {
      var accessToken = await HttpContext.GetTokenAsync("access_token");
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(LoginViewModel model)
    {
      var test = model.UserName;
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
