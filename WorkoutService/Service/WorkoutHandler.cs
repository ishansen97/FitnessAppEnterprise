using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutService.Context;
using WorkoutService.Entity;
using WorkoutService.Entity.Enums;
using WorkoutService.Extensions;
using WorkoutService.Helpers;
using WorkoutService.Model;
using WorkoutService.Repository.implementations;
using WorkoutService.Service.Interfaces;

namespace WorkoutService.Service
{
  public class WorkoutHandler : EntityBaseRepository<Workout>, IWorkoutService
  {
    private readonly WorkoutTypeHelper _workoutTypeHelper;
    private readonly WorkoutModelHelper _modelHelper;
    private readonly WorkoutDbContext _context;

    public WorkoutHandler(
      WorkoutTypeHelper workoutTypeHelper,
      WorkoutDbContext context,
      WorkoutModelHelper modelHelper)
        : base(context)
    {
      _workoutTypeHelper = workoutTypeHelper;
      _context = context;
      _modelHelper = modelHelper;
    }

    public IEnumerable<WorkoutTypeModel> GetWorkoutTypes()
    {
      string[] enumValues = Enum.GetNames(typeof(WorkoutType));
      var workoutTypeModels = new List<WorkoutTypeModel>();
      int index = 1;
      foreach (var enumValue in enumValues)
      {
        workoutTypeModels.Add(new WorkoutTypeModel()
        {
          Id = index,
          Name = enumValue
        });
        index++;
      }

      return workoutTypeModels;
    }

    public async Task<IEnumerable<WorkoutAddModel>> GetUserAddWorkoutsAsync(string userId)
    {
      var workouts = await _context.Workouts
                                              .Where(wk => wk.UserId == userId)
                                              .ToListAsync();

      var models = _modelHelper.GetWorkoutModelsByWorkouts(workouts);
      return models;
    }

    public Dictionary<string, double> GetWorkoutTypeFields(int workoutTypeId)
    {
      return _workoutTypeHelper.GetFieldsForWorkoutType(workoutTypeId);
    }

    public async Task SaveWorkout(WorkoutAddModel model)
    {
      var workout = _modelHelper.GetWorkoutByModel(model);

      await AddAsync(workout);
    }

    public async Task UpdateWorkoutAsync(int id, WorkoutEditModel model)
    {
      var workout = new Workout()
      {
        Id = id,
        UserId = model.UserId,
        Created = model.Created,
        Fields = model.Fields.SerializeDict(),
        WorkoutType = Enum.Parse<WorkoutType>(model.WorkoutType)
      };

      await UpdateAsync(id, workout);
    }

    public async Task<WorkoutAddModel> GetWorkoutModelAsync(int id)
    {
      var workout = await GetByIdAsync(id);
      return _modelHelper.GetWorkoutModelByEntity(workout);
    }

    public async Task<int> GetWorkoutCountAsync(string userId)
    {
      var workoutCount = await GetEntitiesAsync(workout => workout.UserId == userId);
      return workoutCount.ToList().Count;
    }

    public async Task<IEnumerable<DetailModel>> GetDetailModelsForUser(string userId)
    {
      var workouts = await GetEntitiesAsync(workout => workout.UserId == userId);
      return _modelHelper.GetDetailModels(workouts);
    }

    public async Task<IEnumerable<WorkoutEditModel>> GetUserWorkoutsAsync(string userId)
    {
      var workouts = await GetEntitiesAsync(workout => workout.UserId == userId);
      var editModels = _modelHelper.GetWorkoutEditModelsFromEntity(workouts.ToList());
      return editModels;
    }

    public async Task<WorkoutEditModel> GetEditDetails(int id)
    {
      var workouts = await GetEntitiesAsync(workout => workout.Id == id);
      var workout = workouts.First();
      return _modelHelper.GetWorkoutEditModel(workout);
    }

    public async Task<IEnumerable<WorkoutEditModel>> GetWeeklyWorkouts(string userId, ActivityAccessModel accessModel)
    {
      var weeklyWorkouts = await GetEntitiesAsync(workout =>
        (workout.Created >= accessModel.StartDate) && (workout.Created <= accessModel.EndDate));
      if (weeklyWorkouts.Any())
      {
        return _modelHelper.GetWorkoutEditModelsFromEntity(weeklyWorkouts.ToList());
      }

      return null;
    }
  }
}
