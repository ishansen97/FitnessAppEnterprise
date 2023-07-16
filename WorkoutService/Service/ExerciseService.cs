using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutService.Context;
using WorkoutService.Entity;
using WorkoutService.Entity.Enums;
using WorkoutService.Helpers;
using WorkoutService.Model;
using WorkoutService.Service.Interfaces;

namespace WorkoutService.Service
{
  public class ExerciseService : IExerciseService
  {
    private readonly WorkoutDbContext _context;
    private readonly WorkoutModelHelper _modelHelper;

    public ExerciseService(
      WorkoutDbContext context,
      WorkoutModelHelper modelHelper)
    {
      _context = context;
      _modelHelper = modelHelper;
    }

    public async Task<ExerciseMeasurementModel> GetExerciseMeasurement(string workoutType)
    {
      var actualWorkoutType = Enum.Parse<WorkoutType>(workoutType);
      var measurement = await _context.ExerciseMeasurements.Where(mea => mea.WorkoutType == actualWorkoutType)
        .FirstOrDefaultAsync();
      if (measurement != null)
      {
        return _modelHelper.GetExerciseMeasurementModelFromEntity(measurement);
      }
      return null;
    }
  }
}
