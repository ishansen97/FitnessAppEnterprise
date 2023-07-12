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

    Task<IEnumerable<WorkoutModel>> GetUserWorkoutsAsync(string userId);

    Task SaveWorkout(WorkoutModel model);

    Task UpdateWorkoutAsync(int id, WorkoutModel model);

    Task<WorkoutModel> GetWorkoutModelAsync(int id);
  }
}
