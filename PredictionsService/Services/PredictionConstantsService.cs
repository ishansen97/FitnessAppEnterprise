using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PredictionsService.Context;
using PredictionsService.Entity;
using PredictionsService.Helpers;
using PredictionsService.Model;
using PredictionsService.Repository.Implementations;
using PredictionsService.Services.Interfaces;

namespace PredictionsService.Services
{
  public class PredictionConstantsService : EntityBaseRepository<PredictionConstant>, IPredictionConstantsService
  {
    private readonly ModelHelper _modelHelper;

    public PredictionConstantsService(
      PredictionDbContext context, 
      ModelHelper modelHelper) 
        : base(context)
    {
      _modelHelper = modelHelper;
    }

    public async Task<PredictionConstantsModel> GetConstant(string name)
    {
      var entities = await GetEntitiesAsync(pred => pred.Name == name);
      if (entities.Any())
      {
        var entity = entities.ToList().First();
        return _modelHelper.GetPredictionConstantModelFromEntity(entity);
      }

      return null;
    }
  }
}
