using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FitnessAppEnterprise.Helpers;
using FitnessAppEnterprise.Models;
using FitnessAppEnterprise.Models.Enums;
using FitnessAppEnterprise.Services.Interfaces;

namespace FitnessAppEnterprise.Controllers
{
  public class WeeklyViewController : Controller
  {
    private readonly IRemoteService _remoteService;
    private readonly DateHelper _dateHelper;

    public WeeklyViewController(
      IRemoteService remoteService,
      DateHelper dateHelper)
    {
      _remoteService = remoteService;
      _dateHelper = dateHelper;
    }

    [HttpGet]
    public async Task<IActionResult> Index(DateTime date)
    {
      if (date == DateTime.MinValue)
      {
        date = DateTime.Today;
      }

      if (date > DateTime.Today)
      {
        date = DateTime.Today;
        ViewData["disableRightBtn"] = true;
      }
      var weeklyText = _dateHelper.CreateWeekText(date);
      ViewData["weeklyText"] = weeklyText;
      ViewData["date"] = date;

      // make the request
      var request = new WeeklyViewRequestModel()
      {
        Date = date,
        UserId = GetUserId()
      };

      var response =
        await _remoteService.PostDataWithSpecialParamsAsync<WeeklyViewRequestModel, List<DailyDetailModel>>(
          EndpointType.WeeklyViews, request, "details");

      if (!response.Any())
      {
        ViewData["responseFound"] = false;
      }
      return View();
    }

    private string GetUserId()
    {
      return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
  }
}
