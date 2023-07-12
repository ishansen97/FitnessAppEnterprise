using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using WorkoutService.Entity.Enums;
using WorkoutService.Model;
using WorkoutService.Service;
using WorkoutService.Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkoutService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class WorkoutTypesController : ControllerBase
  {
    private readonly IWorkoutService _workoutService;

    public WorkoutTypesController(IWorkoutService workoutService)
    {
      _workoutService = workoutService;
    }

    // GET: api/<WorkoutTypesController>
    [Authorize]
    [HttpGet]
    public async Task<IEnumerable<WorkoutTypeModel>> Get()
    {
      var workoutTypes = _workoutService.GetWorkoutTypes();
      return workoutTypes;
    }

    // GET api/<WorkoutTypesController>/5
    [HttpGet("{id}")]
    [Authorize]
    public Dictionary<string, double> Get(int id)
    {
      return _workoutService.GetWorkoutTypeFields(id);
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
