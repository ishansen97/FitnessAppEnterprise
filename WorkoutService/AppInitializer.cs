using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WorkoutService.Context;
using WorkoutService.Entity;
using WorkoutService.Entity.Enums;

namespace WorkoutService
{
  public static class AppInitializer
  {
    public static void Seed(IApplicationBuilder builder)
    {
      using (var scope = builder.ApplicationServices.CreateScope())
      {
        var context = scope.ServiceProvider.GetRequiredService<WorkoutDbContext>();
        context.Database.Migrate();

        if (!context.ExerciseMeasurements.Any())
        {
          var measurement1 = new ExerciseMeasurement
          {
            WorkoutType = WorkoutType.Walking,
            Value = 25
          };
          var measurement2 = new ExerciseMeasurement
          {
            WorkoutType = WorkoutType.Running,
            Value = 45
          };
          var measurement3 = new ExerciseMeasurement
          {
            WorkoutType = WorkoutType.Cycling,
            Value = 40
          };
          var measurement4 = new ExerciseMeasurement
          {
            WorkoutType = WorkoutType.Pushups,
            Value = 1
          };
          var measurement5 = new ExerciseMeasurement
          {
            WorkoutType = WorkoutType.Pullups,
            Value = 2
          };
          var measurement6 = new ExerciseMeasurement
          {
            WorkoutType = WorkoutType.Crunches,
            Value = 25
          };
          var measurement7 = new ExerciseMeasurement
          {
            WorkoutType = WorkoutType.Plank,
            Value = 8
          };
          var measurement8 = new ExerciseMeasurement
          {
            WorkoutType = WorkoutType.Squats,
            Value = 1
          };
          var measurement9 = new ExerciseMeasurement
          {
            WorkoutType = WorkoutType.DeadLifts,
            Value = 10
          };
          var measurement10 = new ExerciseMeasurement
          {
            WorkoutType = WorkoutType.BicepCurls,
            Value = 3
          };
          var measurement11 = new ExerciseMeasurement
          {
            WorkoutType = WorkoutType.BenchPress,
            Value = 8
          };
          var measurement12 = new ExerciseMeasurement
          {
            WorkoutType = WorkoutType.LegPress,
            Value = 10
          };

          context.AddRange(measurement1, measurement2, measurement3, measurement4, measurement5, measurement6,
                          measurement7, measurement8, measurement9, measurement10, measurement11, measurement12);

          context.SaveChanges();
        }

        if (!context.MealMeasurements.Any())
        {
          var meal1 = new MealMeasurement
          {
            MealType = MealType.Pizza,
            Value = 2.7
          };
          var meal2 = new MealMeasurement
          {
            MealType = MealType.Pasta,
            Value = 1.31
          };
          var meal3 = new MealMeasurement
          {
            MealType = MealType.Bacon,
            Value = 5
          };
          var meal4 = new MealMeasurement
          {
            MealType = MealType.Burger,
            Value = 2.95
          };
          var meal5 = new MealMeasurement
          {
            MealType = MealType.Taco,
            Value = 2.25
          };

          context.MealMeasurements.AddRange(meal1, meal2, meal3, meal4, meal5);
          context.SaveChanges();
        }
      }
    }
  }
}
