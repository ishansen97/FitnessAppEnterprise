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

    public DbSet<ExerciseMeasurement> ExerciseMeasurements { get; set; }

    public DbSet<MealMeasurement> MealMeasurements { get; set; }

    public WorkoutDbContext(DbContextOptions<WorkoutDbContext> options)
      : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<ExerciseMeasurement>().HasIndex(ex => ex.WorkoutType).IsUnique();
      modelBuilder.Entity<MealMeasurement>().HasIndex(ex => ex.MealType).IsUnique();
      base.OnModelCreating(modelBuilder);
    }
  }
}
