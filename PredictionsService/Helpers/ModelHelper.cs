using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PredictionsService.Entity;
using PredictionsService.Model;
using PredictionsService.Model.Enums;

namespace PredictionsService.Helpers
{
  public class ModelHelper
  {
    public PredictionConstantsModel GetPredictionConstantModelFromEntity(PredictionConstant predictionConstant)
    {
      var model = new PredictionConstantsModel()
      {
        Id = predictionConstant.Id,
        Name = predictionConstant.Name,
        Value = predictionConstant.Value
      };

      return model;
    }

    public PredictionModel GetPredictionModelFromEntity(Prediction prediction)
    {
      var model = new PredictionModel
      {
        UserId = prediction.UserId,
        BMI = prediction.BMI,
        PredictedBMI = prediction.PredictedBMI,
        PredictedWeight = prediction.PredictedWeight,
        PredictedDate = prediction.PredictedDate,
        CurrentWeight = prediction.CurrentWeight,
        State = prediction.State.ToString(),
        WeightStatus = prediction.WeightStatus.ToString(),
        Message = prediction.Message
      };

      return model;
    }

    public string GetEndpoint(EndpointType endpoint)
    {
      string endpointUrl = string.Empty;
      switch (endpoint)
      {
        case EndpointType.Workout:
          //endpointUrl = "workout";
          endpointUrl = "WorkoutService";
          break;
        case EndpointType.WorkoutTypes:
          //endpointUrl = "workouttypes";
          endpointUrl = "WorkoutTypesService";
          break;
        case EndpointType.Predictions:
          //endpointUrl = "predictions";
          endpointUrl = "PredictionsService";
          break;
        case EndpointType.Reports:
          //endpointUrl = "report";
          endpointUrl = "ReportService";
          break;
        case EndpointType.WeeklyViews:
          //endpointUrl = "weeklyview";
          endpointUrl = "WeeklyViewService";
          break;
        case EndpointType.CheatMeals:
          //endpointUrl = "cheatmeals";
          endpointUrl = "CheatMealsService";
          break;
        case EndpointType.CheatMealTypes:
          //endpointUrl = "cheatmealtypes";
          endpointUrl = "CheatMealTypesService";
          break;
        case EndpointType.User:
          endpointUrl = "AuthenticationService";
          break;
      }

      return endpointUrl;
    }

    public string GetResourceByHttpMethod(HttpMethod httpMethod)
    {
      string resource = string.Empty;
      if (httpMethod == HttpMethod.Get)
      {
        resource = "";
      }
      else if (httpMethod == HttpMethod.Post)
      {
        resource = "add";
      }
      else if (httpMethod == HttpMethod.Put)
      {
        resource = "update";
      }
      else if (httpMethod == HttpMethod.Delete)
      {
        resource = "delete";
      }

      return resource;
    }
  }
}
