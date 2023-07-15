using Microsoft.AspNetCore.Mvc;
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
    public string Get(int id)
    {
      return "value";
    }

    // GET api/<PredictionsController>/predict/
    [HttpPost("predict/")]
    public async Task<ActionResult<PredictionModel>> MakePrediction([FromBody]PredictionAccessModel accessModel)
    {
      var model = await _predictionService.GetUserPredictionAsync(accessModel);
      if (model == null) return NotFound();
      return model;
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

    // POST api/<PredictionsController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
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
