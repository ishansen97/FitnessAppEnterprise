using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FitnessAppEnterprise.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
        WorkoutTypes = models
      };

      return View(workoutModel);
    }
  }
}
