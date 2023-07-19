using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FitnessAppEnterprise.Extensions;
using FitnessAppEnterprise.Helpers;
using FitnessAppEnterprise.Models;
using FitnessAppEnterprise.Models.Enums;
using FitnessAppEnterprise.Services.Interfaces;
using Microsoft.AspNetCore.Http;

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
      else
      {
        HttpContext.Session.SetString("weeklyModel", "");
        HttpContext.Session.SetObject("weeklyModel", response);
      }
      return View(response);
    }

    [HttpGet]
    public async Task<IActionResult> Report(DateTime date)
    {
      var weeklyModels = HttpContext.Session.GetObject<List<DailyDetailModel>>("weeklyModel");
      var model = weeklyModels.Where(wk => wk.Created == date).ToList().FirstOrDefault();

      if (model != null)
      {
        var request = new CreateReportRequestModel()
        {
          UserId = GetUserId(),
          Created = date,
          Workouts = model.Workouts,
          CheatMeals = model.CheatMeals
        };

        var response = await _remoteService.PostDataWithSpecialParamsAsync<CreateReportRequestModel, ReportResponseModel>(EndpointType.Reports, 
                              request, "add");

        if (response != null)
        {
          var dateTxt = _dateHelper.GetMonthAndDateNoGap(response.Created);
          ViewData["reportModalId"] = $"report{dateTxt}";
          return View(response);
        }
      }
      return RedirectToAction(nameof(Index));
    }

    private string GetUserId()
    {
      return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
  }
}
