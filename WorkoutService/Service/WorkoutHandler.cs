using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutService.Entity;
using WorkoutService.Entity.Enums;

namespace WorkoutService.Service
{
    public class WorkoutHandler
    {
      public WorkoutHandler()
      {
        
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
    }
}
