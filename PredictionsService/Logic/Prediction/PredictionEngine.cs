using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PredictionsService.Constants;
using PredictionsService.Entity.Enum;
using PredictionsService.Logic.Calorie;
using PredictionsService.Model;
using PredictionsService.Services.Interfaces;

namespace PredictionsService.Logic.Prediction
{
  public class PredictionEngine
  {
    private readonly CalorieCounter _calorieCounter;
    private readonly IPredictionConstantsService _constantsService;

    public PredictionEngine(
      CalorieCounter calorieCounter,
      IPredictionConstantsService constantsService)
    {
      _calorieCounter = calorieCounter;
      _constantsService = constantsService;
    }

    public async Task<Entity.Prediction> MakePredictionAsync(List<WorkoutModel> workouts, List<CheatMealModel> cheatMeals, 
                                              AppUserModel user, DateTime predictedDateTime)
    {
      var workoutCalories = await _calorieCounter.GetTotalWorkoutCalorieExpenditure(workouts, user);
      var cheatMealCalories = await _calorieCounter.GetTotalCheatMealCalorieIntake(cheatMeals);
      var calorieDifference = workoutCalories - cheatMealCalories;
      var calorieExpenditurePerDay = _calorieCounter.GetCalorieExpenditurePerDay(workouts, cheatMeals, calorieDifference);

      var dateDiff = (int)(predictedDateTime - DateTime.Today).TotalDays;
      var totalCalorieExpenditure = _calorieCounter.CalculateTotalCalorieExpenditure(user, dateDiff, calorieExpenditurePerDay);

      var prediction = new Entity.Prediction()
      {
        UserId = user.UserId,
        State = (calorieDifference > 0) ? PredictionState.Good : PredictionState.Bad,
        Message = GetPredictionMessage(user, calorieDifference > 0),
        PredictedDate = predictedDateTime,
        CurrentWeight = user.Weight,
        PredictedWeight = await GetPredictedWeight(user, totalCalorieExpenditure),
        BMI = GetBMI(user)
      };

      prediction.PredictedBMI = GetPredictedBMI(user, prediction.PredictedWeight);
      prediction.WeightStatus = GetPredictedWeightStatus(prediction.PredictedBMI);

      return prediction;
    }

    private double GetPredictedBMI(AppUserModel userModel, double predictedWeight)
    {
      var user = userModel;
      var heightInMetres = user.Height / 100.0;
      var predictedBMI = predictedWeight / Math.Pow(heightInMetres, 2);

      return predictedBMI;
    }

    private double GetBMI(AppUserModel userModel)
    {
      var user = userModel;
      var heightInMetres = user.Height / 100.0;
      var BMI = user.Weight / Math.Pow(heightInMetres, 2);

      return BMI;
    }

    private async Task<double> GetPredictedWeight(AppUserModel userModel, double totalCalorieExpenditure)
    {
      var constantModel = await _constantsService.GetConstant(AppConstants.KiloPerCalorie);
      var kiloPerCalorie = double.Parse(constantModel.Value);
      double kiloCalorieExpenditure = Math.Round(totalCalorieExpenditure * kiloPerCalorie, 1);
      return userModel.Weight - kiloCalorieExpenditure;
    }

    private string GetPredictionMessage(AppUserModel userModel, bool isGood)
    {
      string message = string.Empty;
      if (isGood)
      {
        message = $"You are losing weight {userModel.FirstName}. Good job";
      }
      else
      {
        message = $"{userModel.FirstName} you have gained weight. Please try to be more active.";
      }

      return message;
    }

    private PredictedWeightStatus GetPredictedWeightStatus(double predictedBMI)
    {
      if (predictedBMI < 18.5)
      {
        return PredictedWeightStatus.Underweight;
      }
      if (predictedBMI >= 18.5 && predictedBMI <= 24.9)
      {
        return PredictedWeightStatus.Normal;
      }
      if (predictedBMI >= 25 && predictedBMI <= 29.9)
      {
        return PredictedWeightStatus.Overweight;
      }
      if (predictedBMI >= 30 && predictedBMI <= 34.9)
      {
        return PredictedWeightStatus.Obese;
      }
      if (predictedBMI >= 35)
      {
        return PredictedWeightStatus.ExtremelyObese;
      }

      return PredictedWeightStatus.None;
    }
  }
}
