using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PredictionsService.Context;
using PredictionsService.Entity;
using PredictionsService.Entity.Enum;
using PredictionsService.Helpers;
using PredictionsService.Logic.Prediction;
using PredictionsService.Model;
using PredictionsService.Model.Enums;
using PredictionsService.Repository.Implementations;
using PredictionsService.Services.Interfaces;

namespace PredictionsService.Services
{
    public class PredictionService : EntityBaseRepository<Prediction>, IPredictionService
    {
      private readonly ModelHelper _modelHelper;
      private readonly IRemoteService _remoteService;
      private readonly PredictionEngine _predictionEngine;

      public PredictionService(
        PredictionDbContext context,
        ModelHelper modelHelper, 
        IRemoteService remoteService, 
        PredictionEngine predictionEngine) 
          : base(context)
      {
        _modelHelper = modelHelper;
        _remoteService = remoteService;
        _predictionEngine = predictionEngine;
      }

      public async Task<PredictionModel> GetUserPredictionAsync(PredictionAccessModel accessModel)
      {
        // create the prediction based on access model details.
        var userId = accessModel.UserId;
        var user = await _remoteService.GetSingleModelDataAsync<AppUserModel>(EndpointType.User, HttpMethod.Get,
          param: accessModel.UserId);
        var userWorkouts =
          await _remoteService.GetMultipleModelDataAsync<WorkoutModel>(EndpointType.Workout, HttpMethod.Get, "user", userId);
        var userCheatMeals =
          await _remoteService.GetMultipleModelDataAsync<CheatMealModel>(EndpointType.CheatMeals, HttpMethod.Get, "user", userId);

        var prediction = await 
          _predictionEngine.MakePredictionAsync(userWorkouts, userCheatMeals, user, accessModel.PredictionDate);
        var model = _modelHelper.GetPredictionModelFromEntity(prediction);
        return model;
      }

      public async Task CreateUserPrediction(PredictionModel model)
      {
        var entity = _modelHelper.GetPredictionFromModel(model);
        await AddAsync(entity);
      }

      public async Task<PredictionModel> GetPredictionById(int id)
      {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
          return _modelHelper.GetPredictionModelFromEntity(entity);
        }

        return null;
      }
    }
}
