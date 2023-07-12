using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutService.Entity;
using WorkoutService.Model;
using WorkoutService.Repository.Interfaces;

namespace WorkoutService.Service.Interfaces
{
  public interface ICheatMealService : IEntityBaseRepository<CheatMeal>
  {
    IEnumerable<CheatMealTypeModel> GetCheatMealTypes();
  }
}
