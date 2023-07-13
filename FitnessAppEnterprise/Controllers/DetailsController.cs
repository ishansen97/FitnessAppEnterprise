using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
  public class DetailsController : Controller
  {
    private readonly IRemoteService _remoteService;

    public DetailsController(IRemoteService remoteService)
    {
      _remoteService = remoteService;
    }

    // GET: DetailsController
    public ActionResult Index()
    {
      return View();
    }

    // GET: DetailsController/Details/5
    public async Task<ActionResult> Details(int id)
    {
      var model = new List<DetailModel>();
      var userId = GetUserId();

      if (id == (int)ActivityType.Workout)
      {
        model = await _remoteService.GetDetailModels(EndpointType.Workout, userId);
      }
      else if (id == (int)ActivityType.CheatMeals)
      {
        model = await _remoteService.GetDetailModels(EndpointType.CheatMeals, userId);
      }

      return View(model);
    }

    // GET: DetailsController/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: DetailsController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormCollection collection)
    {
      try
      {
        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View();
      }
    }

    // GET: DetailsController/Edit/5
    public ActionResult Edit(int id)
    {
      return View();
    }

    // POST: DetailsController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
      try
      {
        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View();
      }
    }

    // GET: DetailsController/Delete/5
    public async Task<ActionResult> Delete(int id, [FromQuery] int activity)
    {
      HttpResponseMessage message = null;
      switch (activity)
      {
        case 1:
          message = await _remoteService.DeleteAsync(EndpointType.Workout, id);
          break;
        case 2:
          message = await _remoteService.DeleteAsync(EndpointType.CheatMeals, id);
          break;
      }

      if (message == null)
      {
        return NotFound();
      }

      if (message.StatusCode == HttpStatusCode.Accepted)
      {
        return RedirectToAction("Index", "Home");
      }

      return BadRequest();
    }

    // POST: DetailsController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
      try
      {
        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View();
      }
    }

    private string GetUserId()
    {
      return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
  }
}
