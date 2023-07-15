using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PredictionsService.Entity;
using PredictionsService.Model;

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
  }
}
