using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutService.Entity;
using WorkoutService.Entity.Enums;
using WorkoutService.Extensions;
using WorkoutService.Model;

namespace WorkoutService.Helpers
{
  public class WorkoutModelHelper
  {
    public Workout GetWorkoutByModel(WorkoutModel model)
    {
      var workout = new Workout()
      {
        UserId = model.UserId,
        Created = DateTime.Today,
        Fields = model.Fields.SerializeDict(),
        WorkoutType = (WorkoutType)model.SelectedWorkoutType
      };

      return workout;
    }

    public List<Workout> GetWorkoutsByModels(List<WorkoutModel> models)
    {
      var workouts = new List<Workout>();
      foreach (var model in models)
      {
        workouts.Add(GetWorkoutByModel(model));
      }

      return workouts;
    }

    public WorkoutModel GetWorkoutModelByEntity(Workout workout)
    {
      var workoutModel = new WorkoutModel()
      {
        UserId = workout.UserId,
        Created = workout.Created,
        Fields = workout.Fields.DeserializeDict<string, double>(),
        SelectedWorkoutType = (int)workout.WorkoutType,
      };

      return workoutModel;
    }

    public List<WorkoutModel> GetWorkoutModelsByWorkouts(List<Workout> workouts)
    {
      var models = new List<WorkoutModel>();
      foreach (var workout in workouts)
      {
        models.Add(GetWorkoutModelByEntity(workout));
      }

      return models;
    }
  }
}
