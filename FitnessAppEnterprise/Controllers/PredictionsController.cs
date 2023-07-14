using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using FitnessAppEnterprise.Models;
using FitnessAppEnterprise.Models.Enums;
using FitnessAppEnterprise.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

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

      return View();
    }

    private string GetUserId()
    {
      var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      return userId;
    }
  }
}
