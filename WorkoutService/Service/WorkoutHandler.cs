using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutService.Entity;
using WorkoutService.Entity.Enums;
using WorkoutService.Helpers;
using WorkoutService.Model;

namespace WorkoutService.Service
{
    public class WorkoutHandler
    {
      private readonly WorkoutTypeHelper _workoutTypeHelper;

      public WorkoutHandler(WorkoutTypeHelper workoutTypeHelper)
      {
        _workoutTypeHelper = workoutTypeHelper;
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

      public IEnumerable<Workout> GetUserWorkouts(int userId)
      {
        return new List<Workout>();
      }

      public Dictionary<string, double> GetWorkoutTypeFields(int workoutTypeId)
      {
        return _workoutTypeHelper.GetFieldsForWorkoutType(workoutTypeId);
      }
    }
}
