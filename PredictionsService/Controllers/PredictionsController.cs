﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using PredictionsService.Constants;
using PredictionsService.Model;
using PredictionsService.Services;
using PredictionsService.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PredictionsService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class PredictionsController : ControllerBase
  {
    private readonly IPredictionConstantsService _constantsService;
    private readonly IPredictionService _predictionService;

    public PredictionsController(
      IPredictionConstantsService constantsService, 
      IPredictionService predictionService)
    {
      _constantsService = constantsService;
      _predictionService = predictionService;
    }

    // GET: api/<PredictionsController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
    }

    // GET api/<PredictionsController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PredictionModel>> Get(int id)
    {
      var model = await _predictionService.GetPredictionById(id);
      if (model == null) return NotFound();
      return model;
    }

    // GET api/<PredictionsController>/predict/
    [HttpPost("predict/")]
    public async Task<ActionResult<PredictionModel>> MakePrediction([FromBody]PredictionAccessModel accessModel)
    {
      var model = await _predictionService.GetUserPredictionAsync(accessModel);
      if (model == null) return NotFound();
      return model;
    }

    // POST api/<PredictionsController>/calorie/{userId}
    [HttpPost("calorie/{userId}")]
    public async Task<ActionResult<CalorieResponseModel>> GetCalorieCalculations(string userId, [FromBody] CalorieRequestModel request)
    {
      if (request == null) return BadRequest();
      var response = await _predictionService.GetCalorieDetails(userId, request);
      return response;
    }

    // GET api/<PredictionsController>/minimumCount
    [HttpGet("minimumCount/")]
    public async Task<ActionResult<PredictionConstantsModel>> GetMinimumCount()
    {
      var model = await _constantsService.GetConstant(AppConstants.MinimumActivityCount);
      if (model == null)
        return NotFound();
      return model;
    }

    // POST api/<PredictionsController>/add
    [HttpPost("add")]
    public async Task<ActionResult> Post([FromBody] PredictionModel model)
    {
      if (model == null) return BadRequest();
      try
      {
        await _predictionService.CreateUserPrediction(model);
      }
      catch (Exception ex)
      {
        return NotFound(ex);
      }

      return Ok();
    }

    // PUT api/<PredictionsController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<PredictionsController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
