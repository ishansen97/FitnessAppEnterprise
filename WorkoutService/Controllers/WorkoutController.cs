using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WorkoutService.Model;
using WorkoutService.Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkoutService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class WorkoutController : ControllerBase
  {
    private readonly IWorkoutService _workoutService;

    public WorkoutController(IWorkoutService workoutService)
    {
      _workoutService = workoutService;
    }

    // GET: api/<WorkoutController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
    }

    // GET api/<WorkoutController>/5
    [HttpGet("{id}")]
    public async Task<WorkoutModel> Get(int id)
    {
      var workoutModel = await _workoutService.GetWorkoutModelAsync(id);
      return workoutModel;
    }

    // POST api/<WorkoutController>
    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> Post([FromBody] WorkoutModel model)
    {
      if (model == null) return BadRequest();
      await _workoutService.SaveWorkout(model);
      return Ok();
    }

    // PUT api/<WorkoutController>/5
    [HttpPut("update/{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] WorkoutModel model)
    {
      await _workoutService.UpdateWorkoutAsync(id, model);
      return Ok();
    }

    // DELETE api/<WorkoutController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
