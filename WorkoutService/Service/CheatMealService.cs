using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutService.Context;
using WorkoutService.Entity;
using WorkoutService.Entity.Enums;
using WorkoutService.Helpers;
using WorkoutService.Model;
using WorkoutService.Repository.implementations;
using WorkoutService.Service.Interfaces;

namespace WorkoutService.Service
{
  public class CheatMealService : EntityBaseRepository<CheatMeal>, ICheatMealService
  {
    private readonly WorkoutModelHelper _modelHelper;

    public CheatMealService(
      WorkoutDbContext context, 
      WorkoutModelHelper modelHelper) : base(context)
    {
      _modelHelper = modelHelper;
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

    public async Task SaveCheatMeal(CheatMealModel cheatMealModel)
    {
      var cheatMeal = _modelHelper.GetCheatMealByModel(cheatMealModel);
      await AddAsync(cheatMeal);
    }

    public async Task<int> GetCheatMealCount(string userId)
    {
      var cheatMeals = await GetEntitiesAsync(cheatMeal => cheatMeal.UserId == userId);
      return cheatMeals.ToList().Count;
    }

    public async Task<IEnumerable<DetailModel>> GetDetailModelsForUserAsync(string userId)
    {
      var cheatMeals = await GetEntitiesAsync(cheatMeal => cheatMeal.UserId == userId);
      return _modelHelper.GetDetailModels(cheatMeals);
    }
  }
}
