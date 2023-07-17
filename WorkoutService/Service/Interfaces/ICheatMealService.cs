using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkoutService.Entity;
using WorkoutService.Model;
using WorkoutService.Repository.Interfaces;

namespace WorkoutService.Service.Interfaces
{
  public interface ICheatMealService : IEntityBaseRepository<CheatMeal>
  {
    IEnumerable<CheatMealTypeModel> GetCheatMealTypes();

    Task SaveCheatMeal(CheatMealModel cheatMealModel);

    Task<int> GetCheatMealCount(string userId);

    Task<IEnumerable<DetailModel>> GetDetailModelsForUserAsync(string userId);

    Task<CheatMealEditModel> GetEditDetails(int id);

    Task<IEnumerable<CheatMealEditModel>> GetUserCheatMeals(string userId);

    Task UpdateCheatMealAsync(int id, CheatMealEditModel model);

    Task<IEnumerable<CheatMealEditModel>> GetWeeklyCheatMeals(string userId, ActivityAccessModel accessModel);
  }
}
