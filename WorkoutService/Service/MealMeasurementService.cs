using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutService.Context;
using WorkoutService.Entity.Enums;
using WorkoutService.Helpers;
using WorkoutService.Model;
using WorkoutService.Service.Interfaces;

namespace WorkoutService.Service
{
  public class MealMeasurementService : IMealMeasurementService
  {
    private readonly WorkoutDbContext _context;
    private readonly WorkoutModelHelper _modelHelper;

    public MealMeasurementService(
      WorkoutDbContext context,
      WorkoutModelHelper modelHelper)
    {
      _context = context;
      _modelHelper = modelHelper;
    }


    public async Task<MealMeasurementModel> GetMealMeasurement(string mealType)
    {
      var actualMealType = Enum.Parse<MealType>(mealType);
      var measurement = await _context.MealMeasurements.Where(mea => mea.MealType == actualMealType)
        .FirstOrDefaultAsync();
      if (measurement != null)
      {
        return _modelHelper.GetMealMeasurementModelFromEntity(measurement);
      }
      return null;
    }
  }
}
