using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using WorkoutService.Entity;
using WorkoutService.Entity.Enums;
using WorkoutService.Extensions;
using WorkoutService.Model;

namespace WorkoutService.Helpers
{
  public class WorkoutModelHelper
  {
    #region Workout models
    public Workout GetWorkoutByModel(WorkoutAddModel model)
    {
      var workout = new Workout()
      {
        UserId = model.UserId,
        Created = model.Created,
        Fields = model.Fields.SerializeDict(),
        WorkoutType = (WorkoutType)model.SelectedWorkoutType
      };

      return workout;
    }

    public List<Workout> GetWorkoutsByModels(List<WorkoutAddModel> models)
    {
      var workouts = new List<Workout>();
      foreach (var model in models)
      {
        workouts.Add(GetWorkoutByModel(model));
      }

      return workouts;
    }

    public WorkoutAddModel GetWorkoutModelByEntity(Workout workout)
    {
      var workoutModel = new WorkoutAddModel()
      {
        UserId = workout.UserId,
        Created = workout.Created,
        Fields = workout.Fields.DeserializeDict<string, double>(),
        SelectedWorkoutType = (int)workout.WorkoutType,
      };

      return workoutModel;
    }

    public List<WorkoutAddModel> GetWorkoutModelsByWorkouts(List<Workout> workouts)
    {
      var models = new List<WorkoutAddModel>();
      foreach (var workout in workouts)
      {
        models.Add(GetWorkoutModelByEntity(workout));
      }

      return models;
    }

    public WorkoutEditModel GetWorkoutEditModel(Workout workout)
    {
      var workoutEditModel = new WorkoutEditModel
      {
        Id = workout.Id,
        UserId = workout.UserId,
        Created = workout.Created,
        WorkoutType = workout.WorkoutType.ToString(),
        Fields = workout.Fields.DeserializeDict<string, double>()
      };

      return workoutEditModel;
    }

    public Workout GetWorkoutFromEditModel(WorkoutEditModel workoutEditModel)
    {
      var workout = new Workout
      {
        Id = workoutEditModel.Id,
        UserId = workoutEditModel.UserId,
        Created = workoutEditModel.Created,
        WorkoutType = Enum.Parse<WorkoutType>(workoutEditModel.WorkoutType),
        Fields = workoutEditModel.Fields.SerializeDict()
      };

      return workout;
    }


    #endregion

    #region Cheat meal model

    public CheatMeal GetCheatMealByModel(CheatMealModel model)
    {
      var cheatMeal = new CheatMeal()
      {
        UserId = model.UserId,
        Created = model.Created,
        MealType = (MealType)model.SelectedCheatMealType,
        MealAmount = model.MealAmount
      };

      return cheatMeal;
    }

    public List<CheatMeal> GetCheatMealsByModels(List<CheatMealModel> models)
    {
      var cheatMeals = new List<CheatMeal>();
      foreach (var model in models)
      {
        cheatMeals.Add(GetCheatMealByModel(model));
      }

      return cheatMeals;
    }

    public CheatMealModel GetCheatMealModelByEntity(CheatMeal cheatMeal)
    {
      var cheatMealModel = new CheatMealModel()
      {
        UserId = cheatMeal.UserId,
        Created = cheatMeal.Created,
        MealAmount = cheatMeal.MealAmount,
        SelectedCheatMealType = (int)cheatMeal.MealType,
      };

      return cheatMealModel;
    }

    public List<CheatMealModel> GetCheatMealModelsByCheatMeals(List<CheatMeal> cheatMeals)
    {
      var models = new List<CheatMealModel>();
      foreach (var cheatMeal in cheatMeals)
      {
        models.Add(GetCheatMealModelByEntity(cheatMeal));
      }

      return models;
    }

    public CheatMealEditModel GetCheatMealEditModels(CheatMeal cheatMeal)
    {
      var cheatMealEditModel = new CheatMealEditModel
      {
        Id = cheatMeal.Id,
        UserId = cheatMeal.UserId,
        Created = cheatMeal.Created,
        MealType = cheatMeal.MealType.ToString(),
        MealAmount = cheatMeal.MealAmount
      };

      return cheatMealEditModel;
    }

    public CheatMeal GetCheatMealFromEditModel(CheatMealEditModel cheatMealEditModel)
    {
      var workout = new CheatMeal
      {
        Id = cheatMealEditModel.Id,
        UserId = cheatMealEditModel.UserId,
        Created = cheatMealEditModel.Created,
        MealType = Enum.Parse<MealType>(cheatMealEditModel.MealType),
        MealAmount = cheatMealEditModel.MealAmount
      };

      return workout;
    }

    #endregion

    public IEnumerable<DetailModel> GetDetailModels<T>(IEnumerable<T> entities) where T : EntityBase
    {
      var models = entities.ToList().Select(entity => new DetailModel()
      {
        Id = entity.Id,
        Created = entity.Created,
        UserId = entity.UserId,
        ActivityType = GetActivityType(entity),
        Title = GetTitle(entity)
      });

      return models.ToList();
    }

    private string GetTitle<T>(T entity) where T : EntityBase
    {
      if (entity is Workout wk)
      {
        return wk.WorkoutType.ToString();
      }
      if (entity is CheatMeal cheatMeal)
      {
        return cheatMeal.MealType.ToString();
      }

      return string.Empty;
    } 

    private int GetActivityType<T>(T entity) where T : EntityBase
    {
      if (entity is Workout)
      {
        return 1;
      }
      if (entity is CheatMeal)
      {
        return 2;
      }

      return 0;
    } 


  }
}
