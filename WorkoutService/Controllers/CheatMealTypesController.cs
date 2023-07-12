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
  public class CheatMealTypesController : ControllerBase
  {
    private readonly ICheatMealService _cheatMealService;

    public CheatMealTypesController(ICheatMealService cheatMealService)
    {
      _cheatMealService = cheatMealService;
    }

    // GET: api/<CheatMealTypesController>
    [HttpGet]
    public IEnumerable<CheatMealTypeModel> Get()
    {
      return _cheatMealService.GetCheatMealTypes();
    }

    // GET api/<CheatMealTypesController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "value";
    }

    // POST api/<CheatMealTypesController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<CheatMealTypesController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<CheatMealTypesController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
