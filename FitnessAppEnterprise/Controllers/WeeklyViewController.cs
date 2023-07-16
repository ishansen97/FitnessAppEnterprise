using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FitnessAppEnterprise.Services.Interfaces;

namespace FitnessAppEnterprise.Controllers
{
  public class WeeklyViewController : Controller
  {
    private readonly IRemoteService _remoteService;

    public WeeklyViewController(IRemoteService remoteService)
    {
      _remoteService = remoteService;
    }

    public IActionResult Index()
    {
      return View();
    }

    private string GetUserId()
    {
      return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
  }
}
