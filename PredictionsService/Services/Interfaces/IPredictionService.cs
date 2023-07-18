using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PredictionsService.Entity;
using PredictionsService.Model;
using PredictionsService.Repository.Interfaces;

namespace PredictionsService.Services.Interfaces
{
  public interface IPredictionService : IEntityBaseRepository<Prediction>
  {
    Task<PredictionModel> GetUserPredictionAsync(PredictionAccessModel accessModel);

    Task CreateUserPrediction(PredictionModel model);

    Task<PredictionModel> GetPredictionById(int id);

    Task<CalorieResponseModel> GetCalorieDetails(string userId, CalorieRequestModel request);
  }
}
