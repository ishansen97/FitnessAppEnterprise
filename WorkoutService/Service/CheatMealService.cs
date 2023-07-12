using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutService.Context;
using WorkoutService.Entity;
using WorkoutService.Entity.Enums;
using WorkoutService.Model;
using WorkoutService.Repository.implementations;
using WorkoutService.Service.Interfaces;

namespace WorkoutService.Service
{
  public class CheatMealService : EntityBaseRepository<CheatMeal>, ICheatMealService
  {
    public CheatMealService(WorkoutDbContext context) : base(context)
    {
    }

    public IEnumerable<CheatMealTypeModel> GetCheatMealTypes()
    {
      string[] enumValues = Enum.GetNames(typeof(MealType));
      var cheatMealTypeModels = new List<CheatMealTypeModel>();
      int index = 1;
      foreach (var enumValue in enumValues)
      {
        cheatMealTypeModels.Add(new CheatMealTypeModel()
        {
          Id = index,
          Name = enumValue
        });
        index++;
      }

      return cheatMealTypeModels;
    }
  }
}
