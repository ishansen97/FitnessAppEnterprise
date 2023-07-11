using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutService.Entity.Enums;

namespace WorkoutService.Helpers
{
  public class WorkoutTypeHelper
  {
    public Dictionary<string, double> GetFieldsForWorkoutType(int workoutType)
    {
      WorkoutType actualWorkoutType = (WorkoutType)workoutType;
      var fields = new Dictionary<string, double>();

      switch (actualWorkoutType)
      {
        case WorkoutType.Walking:
        case WorkoutType.Running:
        case WorkoutType.Cycling:
          fields = CreateDistanceFields();
          break;
        case WorkoutType.Plank:
          fields = CreateTimeFields();
          break;
        case WorkoutType.BenchPress:
        case WorkoutType.BicepCurls:
        case WorkoutType.DeadLifts:
        case WorkoutType.LegPress:
          fields = CreateCompositeFields();
          break;
        case WorkoutType.Pushups:
        case WorkoutType.Pullups:
        case WorkoutType.Crunches:
        case WorkoutType.Squats:
          fields = CreateRepFields();
          break;
      }

      return fields;
    }

    private Dictionary<string, double> CreateRepFields()
    {
      return new Dictionary<string, double>()
      {
        ["Reps"] = 0
      };
    }

    private Dictionary<string, double> CreateCompositeFields()
    {
      return new Dictionary<string, double>()
      {
        ["Weight"] = 0,
        ["Reps"] = 0
      };
    }

    private Dictionary<string, double> CreateTimeFields()
    {
      return new Dictionary<string, double>()
      {
        ["Time"] = 0
      };
    }

    private Dictionary<string, double> CreateDistanceFields()
    {
      return new Dictionary<string, double>()
      {
        ["Distance"] = 0
      };
    }
  }
}
