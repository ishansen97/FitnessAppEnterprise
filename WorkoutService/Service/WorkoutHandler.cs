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
      private readonly WorkoutModelHelper _workoutModelHelper;
      private readonly WorkoutDbContext _context;

      public WorkoutHandler(
        WorkoutTypeHelper workoutTypeHelper, 
        WorkoutDbContext context, 
        WorkoutModelHelper workoutModelHelper) 
          : base(context)
      {
        _workoutTypeHelper = workoutTypeHelper;
        _context = context;
        _workoutModelHelper = workoutModelHelper;
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

      public async Task<IEnumerable<WorkoutModel>> GetUserWorkoutsAsync(int userId)
      {
        var workouts = await _context.Workouts
                                                .Where(wk => wk.UserId == userId)
                                                .ToListAsync();

        var models = _workoutModelHelper.GetWorkoutModelsByWorkouts(workouts);
        return models;
      }

      public Dictionary<string, double> GetWorkoutTypeFields(int workoutTypeId)
      {
        return _workoutTypeHelper.GetFieldsForWorkoutType(workoutTypeId);
      }

      public async Task SaveWorkout(WorkoutModel model)
      {
        var workout = new Workout()
        {
          UserId = model.UserId,
          Created = DateTime.Today,
          Fields = model.Fields.SerializeDict(),
          WorkoutType = (WorkoutType)model.SelectedWorkoutType
        };

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
        return _workoutModelHelper.GetWorkoutModelByEntity(workout);
      }
    }
}
