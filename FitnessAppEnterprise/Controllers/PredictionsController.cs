using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using FitnessAppEnterprise.Extensions;
using FitnessAppEnterprise.Models;
using FitnessAppEnterprise.Models.Enums;
using FitnessAppEnterprise.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace FitnessAppEnterprise.Controllers
{
  [Authorize]
  public class PredictionsController : Controller
  {
    private readonly IRemoteService _remoteService;

    public PredictionsController(IRemoteService remoteService)
    {
      _remoteService = remoteService;
    }

    public async Task<IActionResult> Predictions()
    {
      var predictionConstantModel =
        await _remoteService.GetSingleModelDataAsync<PredictionConstantModel>(EndpointType.Predictions, HttpMethod.Get,
          "minimumCount");

      var countModel = await _remoteService.GetModelCountsAsync(GetUserId());

      if (predictionConstantModel == null)
        return NotFound();

      var minimumCount = int.Parse(predictionConstantModel.Value);

      if ((countModel.WorkoutCount < minimumCount) &&
          (countModel.CheatMealCount < minimumCount))
      {
        ViewData["isError"] = true;
      }

      // populate with the model.
      var predictionModel = HttpContext.Session.GetObject<PredictionModel>("predictionModel");
      if (predictionModel != null)
      {
        return View(predictionModel);
      }

      return View();
    }

    [HttpPost]
    public async Task<IActionResult> FindPrediction(IFormCollection form)
    {
      var formValue = form["predictionDate"];
      var predictionDate = DateTime.Parse(formValue.ToString());
      var accessModel = new PredictionAccessModel
      {
        UserId = GetUserId(),
        PredictionDate = predictionDate
      };

      var response = await _remoteService.PostDataWithSpecialParamsAsync<PredictionAccessModel,PredictionModel>(
                                        EndpointType.Predictions, accessModel, "predict");

      // set to the session
      HttpContext.Session.SetObject("predictionModel", response);
      return RedirectToAction(nameof(Predictions));
    }

    [HttpPost]
    public async Task<IActionResult> Create(PredictionModel model)
    {
      return View(nameof(Predictions));
    }

    private string GetUserId()
    {
      var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      return userId;
    }
  }
}
