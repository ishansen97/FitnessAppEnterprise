using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutService.Context;
using WorkoutService.Entity;
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

        // PUT: api/CheatMeals/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCheatMeal(int id, CheatMeal cheatMeal)
        {
            if (id != cheatMeal.Id)
            {
                return BadRequest();
            }

            _context.Entry(cheatMeal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheatMealExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CheatMeals
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("add")]
        public async Task<ActionResult<CheatMeal>> PostCheatMeal(CheatMeal cheatMeal)
        {
            _context.CheatMeals.Add(cheatMeal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCheatMeal", new { id = cheatMeal.Id }, cheatMeal);
        }

        // DELETE: api/CheatMeals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CheatMeal>> DeleteCheatMeal(int id)
        {
            var cheatMeal = await _context.CheatMeals.FindAsync(id);
            if (cheatMeal == null)
            {
                return NotFound();
            }

            _context.CheatMeals.Remove(cheatMeal);
            await _context.SaveChangesAsync();

            return cheatMeal;
        }

        private bool CheatMealExists(int id)
        {
            return _context.CheatMeals.Any(e => e.Id == id);
        }
    }
}
