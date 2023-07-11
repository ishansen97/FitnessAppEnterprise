using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using FitnessAppEnterprise.Extensions;
using FitnessAppEnterprise.Models;
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

    public WorkoutController(IHttpClientFactory clientFactory)
    {
      _clientFactory = clientFactory;
    }

    [Authorize]
    public async Task<IActionResult> AddWorkout()
    {
      var client = _clientFactory.CreateClient("workout_client");
      var accessToken = await HttpContext.GetTokenAsync("access_token");
      client.SetBearerToken(accessToken);
      var response = await client.GetAsync("https://localhost:44379/api/workouttypes/");

      var result = await response.Content.ReadAsStringAsync();
      var models = JsonConvert.DeserializeObject<List<WorkoutTypeModel>>(result);

      var workoutModel = new WorkoutViewModel()
      {
        WorkoutTypes = models,
        Fields = new Dictionary<string, double>(),
        Created = DateTime.Today
      };

      ViewBag.WorkoutTypes = models;
      ViewData["model"] = workoutModel;

      HttpContext.Session.SetObject("workoutModel", workoutModel);

      return View(workoutModel);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddWorkout(WorkoutViewModel model)
    {
      if (ModelState.IsValid)
      {
        
      }

      return View();
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

      return View("AddWorkout", workoutModel);
    }
  }
}
