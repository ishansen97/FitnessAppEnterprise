using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutService.Context;
using WorkoutService.Entity;
using WorkoutService.Model;
using WorkoutService.Service.Interfaces;

namespace WorkoutService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CheatMealsController : ControllerBase
  {
    private readonly WorkoutDbContext _context;
    private readonly ICheatMealService _cheatMealService;

    public CheatMealsController(
      WorkoutDbContext context,
      ICheatMealService cheatMealService)
    {
      _context = context;
      _cheatMealService = cheatMealService;
    }

    // GET: api/CheatMeals
    [HttpGet]
    public async Task<IEnumerable<CheatMeal>> GetCheatMeals()
    {
      return await _cheatMealService.GetAllAsync();
    }

    // GET: api/CheatMeals/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CheatMeal>> GetCheatMeal(int id)
    {
      var cheatMeal = await _cheatMealService.GetByIdAsync(id);

      if (cheatMeal == null)
      {
        return NotFound();
      }

      return cheatMeal;
    }

    // GET: api/CheatMeals/details/userId
    [HttpGet("details/{userId}")]
    public async Task<IEnumerable<DetailModel>> GetCheatMealDetailsForUser(string userId)
    {
      var details = await _cheatMealService.GetDetailModelsForUserAsync(userId);
      return details.ToList();
    }

    // GET: api/CheatMeals/editDetail/id
    [HttpGet("editDetail/{id}")]
    public async Task<ActionResult<CheatMealEditModel>> GetEditCheatMealDetails(int id)
    {
      var model = await _cheatMealService.GetEditDetails(id);
      if (model == null) return NotFound();
      return model;
    }

    // PUT: api/CheatMeals/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPut("update/{id}")]
    public async Task<IActionResult> PutCheatMeal(int id, CheatMealEditModel cheatMeal)
    {
      if (id != cheatMeal.Id)
      {
        return BadRequest();
      }

      await _cheatMealService.UpdateCheatMealAsync(id, cheatMeal);
      return Ok();
    }

    // POST: api/CheatMeals
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost("add")]
    public async Task<ActionResult<CheatMeal>> PostCheatMeal(CheatMealModel cheatMealModel)
    {
      if (cheatMealModel == null) return BadRequest();
      await _cheatMealService.SaveCheatMeal(cheatMealModel);

      return CreatedAtAction("GetCheatMeal", new { id = cheatMealModel.Id }, cheatMealModel);
    }

    // DELETE: api/CheatMeals/5
    [HttpDelete("delete/{id}")]
    public async Task<ActionResult> DeleteCheatMeal(int id)
    {
      await _cheatMealService.DeleteAsync(id);
      return Accepted();
    }

    private bool CheatMealExists(int id)
    {
      return _context.CheatMeals.Any(e => e.Id == id);
    }
  }
}
