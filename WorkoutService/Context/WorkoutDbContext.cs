using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutService.Entity;

namespace WorkoutService.Context
{
  public class WorkoutDbContext : DbContext
  {
    public DbSet<Workout> Workouts { get; set; }

    public DbSet<CheatMeal> CheatMeals { get; set; }

    public WorkoutDbContext(DbContextOptions<WorkoutDbContext> options)
      : base(options)
    {

    }
  }
}
