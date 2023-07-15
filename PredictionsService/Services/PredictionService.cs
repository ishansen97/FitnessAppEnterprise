using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PredictionsService.Context;
using PredictionsService.Entity;
using PredictionsService.Entity.Enum;
using PredictionsService.Helpers;
using PredictionsService.Model;
using PredictionsService.Repository.Implementations;
using PredictionsService.Services.Interfaces;

namespace PredictionsService.Services
{
    public class PredictionService : EntityBaseRepository<Prediction>, IPredictionService
    {
      private readonly ModelHelper _modelHelper;

      public PredictionService(
        PredictionDbContext context,
        ModelHelper modelHelper) 
          : base(context)
      {
        _modelHelper = modelHelper;
      }

      public async Task<PredictionModel> GetUserPredictionAsync(PredictionAccessModel accessModel)
      {
        // create the prediction based on access model details.
        var prediction = new Prediction()
        {
          UserId = accessModel.UserId,
          BMI = 23.5,
          CurrentWeight = 80.0,
          PredictedBMI = 24.2,
          PredictedDate = accessModel.PredictionDate,
          PredictedWeight = 83.2,
          State = PredictionState.Bad,
          WeightStatus = PredictedWeightStatus.Overweight,
          Message = "Need to lose weight"
        };
        var model = _modelHelper.GetPredictionModelFromEntity(prediction);
        return model;
      }
    }
}
