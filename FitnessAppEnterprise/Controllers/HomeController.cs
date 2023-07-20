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
using FitnessAppEnterprise.Models.Enums;
using FitnessAppEnterprise.Services.Interfaces;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace FitnessAppEnterprise.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpClientFactory _clientFactory;
    private readonly IRemoteService _remoteService;

    public HomeController(
      ILogger<HomeController> logger,
      IHttpClientFactory clientFactory, 
      IRemoteService remoteService)
    {
      _logger = logger;
      _clientFactory = clientFactory;
      _remoteService = remoteService;
    }

    public IActionResult FirstPage()
    {
      return View();
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
      var userName = GetUserName();
      var userId = GetUserId();
      ViewData["userName"] = userName;

      var countModel = await _remoteService.GetModelCountsAsync(userId);
      if (countModel == null)
      {
        return RedirectToAction(nameof(Logout));
      }
      return View(countModel);
    }

    [HttpGet]
    public async Task<IActionResult> Profile()
    {
      var userModel =
        await _remoteService.GetSingleModelDataAsync<UserModel>(EndpointType.User, HttpMethod.Get, param: GetUserId());
      if (userModel == null)
      {
        return RedirectToAction(nameof(Index));
      }
      return View(userModel);
    }

    [HttpPost]
    public async Task<IActionResult> Profile(UserModel model)
    {
      if (ModelState.IsValid)
      {
        var response = await _remoteService.PutDataAsync(EndpointType.User, GetUserId(), model);
        if (response.IsSuccessStatusCode)
        {
          return RedirectToAction(nameof(Index));
        }
      }
      
      return View(model);
    }



    public IActionResult Logout()
    {
      return SignOut("Cookie", "oidc");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private string GetUserName()
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

      return userName;
    }

    private string GetUserId()
    {
      return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
  }
}
