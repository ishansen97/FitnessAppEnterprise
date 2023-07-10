using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WorkoutService.Entity;
using WorkoutService.Entity.Enums;
using WorkoutService.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkoutService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class WorkoutTypesController : ControllerBase
  {
    private WorkoutHandler _workoutHandler;

    public WorkoutTypesController(WorkoutHandler workoutHandler)
    {
      _workoutHandler = workoutHandler;
    }

    // GET: api/<WorkoutTypesController>
    [HttpGet]
    [Authorize]
    public IEnumerable<WorkoutTypeModel> Get()
    {
      var workoutTypes = _workoutHandler.GetWorkoutTypes();
      return workoutTypes;
    }

    // GET api/<WorkoutTypesController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "value";
    }

    // POST api/<WorkoutTypesController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<WorkoutTypesController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<WorkoutTypesController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
