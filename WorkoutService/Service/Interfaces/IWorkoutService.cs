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
  public interface IWorkoutService : IEntityBaseRepository<Workout>
  {
    IEnumerable<WorkoutTypeModel> GetWorkoutTypes();

    Dictionary<string, double> GetWorkoutTypeFields(int workoutTypeId);

    Task<IEnumerable<WorkoutAddModel>> GetUserWorkoutsAsync(string userId);

    Task SaveWorkout(WorkoutAddModel model);

    Task UpdateWorkoutAsync(int id, WorkoutEditModel model);

    Task<WorkoutAddModel> GetWorkoutModelAsync(int id);

    Task<int> GetWorkoutCountAsync(string userId);

    Task<IEnumerable<DetailModel>> GetDetailModelsForUser(string userId);

    Task<WorkoutEditModel> GetEditDetails(int id);
  }
}
