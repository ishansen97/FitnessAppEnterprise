using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PredictionsService.Entity;
using PredictionsService.Model;
using PredictionsService.Repository.Interfaces;

namespace PredictionsService.Services.Interfaces
{
  public interface IPredictionConstantsService : IEntityBaseRepository<PredictionConstant>
  {
    Task<PredictionConstantsModel> GetConstant(string name);
  }
}
