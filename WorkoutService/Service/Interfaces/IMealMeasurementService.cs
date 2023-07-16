using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutService.Model;

namespace WorkoutService.Service.Interfaces
{
  public interface IMealMeasurementService
  {
    Task<MealMeasurementModel> GetMealMeasurement(string mealType);
  }
}
