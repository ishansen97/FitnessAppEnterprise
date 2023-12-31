﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using FitnessAppEnterprise.Extensions;
using FitnessAppEnterprise.Models;
using FitnessAppEnterprise.Models.Enums;
using FitnessAppEnterprise.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace FitnessAppEnterprise.Controllers
{
  [Authorize]
  public class CheatMealController : Controller
  {
    private readonly IRemoteService _remoteService;

    public CheatMealController(IRemoteService remoteService)
    {
      _remoteService = remoteService;
    }

    [HttpGet]
    public async Task<IActionResult> CheatMealHome()
    {
      var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var cheatMealTypes =
        await _remoteService.GetMultipleModelDataAsync<CheatMealTypeModel>(EndpointType.CheatMealTypes, HttpMethod.Get);

      var cheatMealModel = HttpContext.Session.GetObject<CheatMealAddModel>("cheatMealModel");
      if (cheatMealModel == null)
      {
        cheatMealModel = new CheatMealAddModel()
        {
          UserId = userId,
          CheatMealTypes = cheatMealTypes,
          Created = DateTime.Today,
          MealAmount = 0
        };
      }

      HttpContext.Session.SetObject("cheatMealModel", cheatMealModel);
      return View(cheatMealModel);
    }

    [HttpPost]
    public async Task<IActionResult> CheatMealHome(CheatMealAddModel model)
    {
      if (ModelState.IsValid)
      {
        var response = await _remoteService.PostDataAsync(EndpointType.CheatMeals, model);
        if (response.StatusCode == HttpStatusCode.Created)
        {
          return RedirectToAction("Index", "Home");
        }
      }

      ViewData["isError"] = true;
      return View(model);
    } 
  }
}
