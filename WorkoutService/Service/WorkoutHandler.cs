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

    public async Task<IEnumerable<WorkoutModel>> GetUserWorkoutsAsync(string userId)
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

    public async Task SaveWorkout(WorkoutModel model)
    {
      var workout = _modelHelper.GetWorkoutByModel(model);

      await AddAsync(workout);
    }

    public async Task UpdateWorkoutAsync(int id, WorkoutModel model)
    {
      var workout = new Workout()
      {
        Id = id,
        UserId = model.UserId,
        Created = DateTime.Today,
        Fields = model.Fields.SerializeDict(),
        WorkoutType = (WorkoutType)model.SelectedWorkoutType
      };

      await UpdateAsync(id, workout);
    }

    public async Task<WorkoutModel> GetWorkoutModelAsync(int id)
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
  }
}
