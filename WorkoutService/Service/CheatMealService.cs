using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

    public async Task<CheatMealEditModel> GetEditDetails(int id)
    {
      var cheatMeals = await GetEntitiesAsync(cheatMeal => cheatMeal.Id == id);
      var cheatMeal = cheatMeals.First();
      return _modelHelper.GetCheatMealEditModel(cheatMeal);
    }

    public async Task<IEnumerable<CheatMealEditModel>> GetUserCheatMeals(string userId)
    {
      var cheatMeals = await GetEntitiesAsync(cheatMeal => cheatMeal.UserId == userId);
      var models = _modelHelper.GetCheatMealEditModelsFromEntity(cheatMeals.ToList());
      return models.ToList();
    }

    public async Task UpdateCheatMealAsync(int id, CheatMealEditModel model)
    {
      var cheatMeal = _modelHelper.GetCheatMealFromEditModel(model);
      await UpdateAsync(id, cheatMeal);
    }

    public async Task<IEnumerable<CheatMealEditModel>> GetWeeklyCheatMeals(string userId, ActivityAccessModel accessModel)
    {
      var cheatMeals = await GetEntitiesAsync(cheatMeal =>
        (cheatMeal.Created >= accessModel.StartDate) && (cheatMeal.Created <= accessModel.EndDate));
      if (cheatMeals.Any())
      {
        return _modelHelper.GetCheatMealEditModelsFromEntity(cheatMeals.ToList());
      }

      return null;
    }
  }
}
