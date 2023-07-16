using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutService.Entity;
using WorkoutService.Entity.Enums;
using WorkoutService.Model;

namespace WorkoutService.Service.Interfaces
{
  public interface IExerciseService
  {
    Task<ExerciseMeasurementModel> GetExerciseMeasurement(string workoutType);
  }
}
