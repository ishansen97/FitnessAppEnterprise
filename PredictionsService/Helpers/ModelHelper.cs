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
  }
}
