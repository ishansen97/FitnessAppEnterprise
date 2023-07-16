using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PredictionsService.Constants;
using PredictionsService.Model;
using PredictionsService.Model.Enums;
using PredictionsService.Services.Interfaces;

namespace PredictionsService.Logic.Calorie
{
  public class CalorieCounter
  {
    private readonly IRemoteService _remoteService;
    private readonly IPredictionConstantsService _constantsService;
    private double _calorieWeightFactor = 1;

    public CalorieCounter(
      IRemoteService remoteService, 
      IPredictionConstantsService constantsService)
    {
      _remoteService = remoteService;
      _constantsService = constantsService;
      SetCalorieWeightFactor().Wait();
    }

    private async Task SetCalorieWeightFactor()
    {
      var constantModel = await _constantsService.GetConstant(AppConstants.CalorieWeightFactor);
      _calorieWeightFactor = double.Parse(constantModel.Value);
    }

    public async Task<double> GetTotalWorkoutCalorieExpenditure(List<WorkoutModel> workouts, AppUserModel user)
    {
      //double totalCalories = workouts.Sum(GetCalorieExpenditureForExercise);
      double totalCalories = 0;
      foreach (var workoutModel in workouts)
      {
        totalCalories += await GetCalorieExpenditureForExercise(workoutModel, user);
      }
      return totalCalories;
    }

    public async Task<double> GetTotalCheatMealCalorieIntake(List<CheatMealModel> cheatMeals)
    {
      double totalCalorieIntake = 0;
      foreach (var cheatMealModel in cheatMeals)
      {
        totalCalorieIntake += await GetCalorieIntakeForCheatMeals(cheatMealModel);
      }
      return totalCalorieIntake;
    }

    public double GetCalorieExpenditurePerDay(List<WorkoutModel> workouts, List<CheatMealModel> cheatMeals, double calorieDiff)
    {
      DateTime minDate = new DateTime();
      var workoutsOrderedByDate = workouts.Select(wk => wk.Created).OrderBy(dt => dt).ToList();
      var cheatMealsOrderedByDate = cheatMeals.Select(cm => cm.Created).OrderBy(dt => dt).ToList();
      var minWorkoutDate = DateTime.Today;
      var maxWorkoutDate = DateTime.Today;
      var minCheatMealDate = DateTime.Today;
      var maxCheatMealDate = DateTime.Today;
      if (workoutsOrderedByDate.Any())
      {
        minWorkoutDate = workoutsOrderedByDate.First();
        maxWorkoutDate = workoutsOrderedByDate.Last();
      }

      if (cheatMealsOrderedByDate.Any())
      {
        minCheatMealDate = cheatMealsOrderedByDate.First();
        maxCheatMealDate = cheatMealsOrderedByDate.Last();
      }

      var dateTimes = new List<DateTime> { minWorkoutDate, maxWorkoutDate, minCheatMealDate, maxCheatMealDate };
      minDate = dateTimes.OrderBy(dt => dt).First();

      var dateGap = (DateTime.Today - minDate).TotalDays;

      if (dateGap > 0)
      {
        return Math.Round((calorieDiff / dateGap), 1);
      }

      return 0;
    }

    public double CalculateTotalCalorieExpenditure(AppUserModel user, int dateDiff, double expPerDay)
    {
      var calorieBurnFactor = GetCalorieBurnFactor(user.Weight);
      var totalCalorieExpenditure = calorieBurnFactor * dateDiff * expPerDay;

      return totalCalorieExpenditure;
    }

    private static double GetCalorieBurnFactor(double weight)
    {
      double calorieBurnFactor = 1;
      if (weight >= 200)
      {
        calorieBurnFactor = 2.5;
      }
      else if (weight >= 150 && weight < 200)
      {
        calorieBurnFactor = 2;
      }
      else if (weight >= 100 && weight < 150)
      {
        calorieBurnFactor = 1.5;
      }
      else if (weight >= 50 && weight < 100)
      {
        calorieBurnFactor = 1;
      }
      else
      {
        calorieBurnFactor = 0.5;
      }

      return calorieBurnFactor;
    }

    public async Task<double> GetCalorieExpenditureForExercise(WorkoutModel workout, AppUserModel user)
    {
      double weight = 0.0f;
      double unit = 0, calorieExpenditure = 0;
      var exerciseType = Enum.Parse<ExerciseType>(workout.WorkoutType);
      switch (exerciseType)
      {
        case ExerciseType.Walking:
        case ExerciseType.Running:
        case ExerciseType.Cycling:
          unit = GetWorkoutUnit(workout.Fields, "Distance");
          weight = user.Weight;
          break;
        case ExerciseType.Pushups:
        case ExerciseType.Pullups:
        case ExerciseType.Crunches:
        case ExerciseType.Squats:
          unit = GetWorkoutUnit(workout.Fields, "Reps");
          weight = user.Weight;
          break;
        case ExerciseType.Plank:
          unit = GetWorkoutUnit(workout.Fields, "Time");
          weight = user.Weight;
          break;
        case ExerciseType.DeadLifts:
        case ExerciseType.BicepCurls:
        case ExerciseType.BenchPress:
        case ExerciseType.LegPress:
          unit = GetWorkoutUnit(workout.Fields, "Reps");
          weight = GetWorkoutWeight(workout.Fields);
          break;
      }

      // calculate the calorie expenditure.
      var exerciseMeasurement = await _remoteService.GetSingleModelDataAsync<ExerciseMeasurementModel>(
                                              EndpointType.Workout, HttpMethod.Get,
                                          "exercisemeasurement", workout.WorkoutType);
      var exerciseCalorie = exerciseMeasurement.Value;
      calorieExpenditure = exerciseCalorie * unit * _calorieWeightFactor * weight;
      return calorieExpenditure;
    }

    public async Task<double> GetCalorieIntakeForCheatMeals(CheatMealModel cheatMeal)
    {
      var mealMeasurement = await _remoteService.GetSingleModelDataAsync<ExerciseMeasurementModel>(
        EndpointType.CheatMeals, HttpMethod.Get,
        "mealmeasurement", cheatMeal.MealType);

      var mealCaloriePerUnit = mealMeasurement.Value;
      var calorieIntake = mealCaloriePerUnit * cheatMeal.MealAmount;
      return calorieIntake;
    }

    private static double GetWorkoutWeight(Dictionary<string, double> fields)
    {
      double weight = 0.0f;
      if (fields.ContainsKey("Weight"))
      {
        weight = fields["Weight"];
      }

      return weight;
    }

    private static double GetWorkoutUnit(Dictionary<string, double> fields, string key)
    {
      double unit = 0;
      if (fields.ContainsKey(key))
      {
        unit = fields[key];
      }

      return unit;
    }
  }
}
