using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FitnessAppEnterprise.Extensions;
using FitnessAppEnterprise.Models;
using FitnessAppEnterprise.Models.Enums;
using FitnessAppEnterprise.Services.Implementations;
using FitnessAppEnterprise.Services.Interfaces;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;

namespace FitnessAppEnterprise.Controllers
{
  public class WorkoutController : Controller
  {
    private readonly IHttpClientFactory _clientFactory;
    private readonly IRemoteService _remoteService;

    public WorkoutController(
      IHttpClientFactory clientFactory,
      IRemoteService remoteService)
    {
      _clientFactory = clientFactory;
      _remoteService = remoteService;
    }

    [Authorize]
    public async Task<IActionResult> AddWorkout()
    {
      //var client = _clientFactory.CreateClient("workout_client");
      //var accessToken = await HttpContext.GetTokenAsync("access_token");
      //client.SetBearerToken(accessToken);
      //var response = await client.GetAsync("https://localhost:44379/api/workouttypes/");
      var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var userName = User.FindFirst(ClaimTypes.Email)?.Value;

      //var result = await response.Content.ReadAsStringAsync();
      //var models = JsonConvert.DeserializeObject<List<WorkoutTypeModel>>(result);
      var models = await _remoteService.GetMultipleModelDataAsync<WorkoutTypeModel>(EndpointType.WorkoutTypes, HttpMethod.Get);

      var workoutModel = HttpContext.Session.GetObject<WorkoutViewModel>("workoutModel");
      if (workoutModel == null)
      {
        workoutModel = new WorkoutViewModel()
        {
          UserId = userId,
          WorkoutTypes = models,
          Fields = new Dictionary<string, double>(),
          Created = DateTime.Today
        };
      }

      HttpContext.Session.SetObject("workoutModel", workoutModel);

      return View(workoutModel);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddWorkout(WorkoutViewModel model)
    {
      if (ModelState.IsValid)
      {
        var response = await _remoteService.PostDataAsync(EndpointType.Workout, model);

        if (response.StatusCode == HttpStatusCode.OK)
        {
          HttpContext.Session.Remove("workoutModel");
          return RedirectToAction("Index", "Home");
        }
      }

      return View("Error");
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> WorkoutTypeChanged(WorkoutViewModel model)
    {
      var selectedValue = model.SelectedWorkoutType;
      var client = _clientFactory.CreateClient();
      var accessToken = await HttpContext.GetTokenAsync("access_token");
      client.SetBearerToken(accessToken);
      var response = await client.GetAsync($"https://localhost:44379/api/workouttypes/{selectedValue}");

      var result = await response.Content.ReadAsStringAsync();
      var fields = JsonConvert.DeserializeObject<Dictionary<string, double>>(result);
      var workoutModel = HttpContext.Session.GetObject<WorkoutViewModel>("workoutModel");
      workoutModel.Fields = fields;
      workoutModel.SelectedWorkoutType = selectedValue;
      HttpContext.Session.SetObject("workoutModel", workoutModel);

      return RedirectToAction("AddWorkout");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
