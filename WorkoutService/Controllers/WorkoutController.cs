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
    private readonly ICheatMealService _cheatMealService;
    private readonly IExerciseService _exerciseService;

    public WorkoutController(
      IWorkoutService workoutService, 
      ICheatMealService cheatMealService, 
      IExerciseService exerciseService)
    {
      _workoutService = workoutService;
      _cheatMealService = cheatMealService;
      _exerciseService = exerciseService;
    }

    // GET: api/<WorkoutController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
    }

    // GET api/<WorkoutController>/5
    [HttpGet("{id}")]
    public async Task<WorkoutAddModel> Get(int id)
    {
      var workoutModel = await _workoutService.GetWorkoutModelAsync(id);
      return workoutModel;
    }

    // GET api/<WorkoutController>/count/{userId}
    [HttpGet("count/{userId}")]
    public async Task<CountModel> GetCounts(string userId)
    {
      var workoutCount = await _workoutService.GetWorkoutCountAsync(userId);
      var cheatMealCount = await _cheatMealService.GetCheatMealCount(userId);
      var countModel = new CountModel
      {
        WorkoutCount = workoutCount,
        CheatMealCount = cheatMealCount
      };

      return countModel;
    }

    // GET api/<WorkoutController>/details/{userId}
    [HttpGet("details/{userId}")]
    public async Task<IEnumerable<DetailModel>> GetWorkoutDetailsForUser(string userId)
    {
      var details = await _workoutService.GetDetailModelsForUser(userId);
      return details.ToList();
    }

    // GET api/<WorkoutController>/user/{userId}
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<WorkoutEditModel>>> GetUserWorkouts(string userId)
    {
      var models = await _workoutService.GetUserWorkoutsAsync(userId);
      if (!models.Any())
        return NotFound();
      return models.ToList();
    }

    // GET api/<WorkoutController>/editDetail/{id}
    [HttpGet("editDetail/{id}")]
    public async Task<ActionResult<WorkoutEditModel>> GetEditWorkoutDetails(int id)
    {
      var model = await _workoutService.GetEditDetails(id);
      if (model == null) return NotFound();
      return model;
    }

    // POST api/<WorkoutController>
    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> Post([FromBody] WorkoutAddModel model)
    {
      if (model == null) return BadRequest();
      await _workoutService.SaveWorkout(model);
      return Ok();
    }

    // PUT api/<WorkoutController>/update/5
    [HttpPut("update/{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] WorkoutEditModel model)
    {
      await _workoutService.UpdateWorkoutAsync(id, model);
      return Ok();
    }

    // DELETE api/<WorkoutController>/5
    [HttpDelete("delete/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
      await _workoutService.DeleteAsync(id);
      return Accepted();
    }

    // GET api/<WorkoutController>/exercisemeasurement/{workoutType}
    [HttpGet("exercisemeasurement/{workoutType}")]
    public async Task<ActionResult<ExerciseMeasurementModel>> GetExerciseMeasurement(string workoutType)
    {
      var model = await _exerciseService.GetExerciseMeasurement(workoutType);
      if (model == null) return NotFound();
      return model;
    }
  }
}
