using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PredictionsService.Constants;
using PredictionsService.Context;
using PredictionsService.Entity;

namespace PredictionsService
{
  public static class ContextInitializer
  {
    public static void Seed(IApplicationBuilder builder)
    {
      using (var scope = builder.ApplicationServices.CreateScope())
      {
        var context = scope.ServiceProvider.GetRequiredService<PredictionDbContext>();
        context.Database.Migrate();

        NewConstantAddition(context);
      }
    }

    private static void NewConstantAddition(PredictionDbContext context)
    {
      if (!context.PredictionConstants.Any())
      {
        var predictionConstant = new PredictionConstant()
        {
          Name = AppConstants.MinimumActivityCount,
          Value = "3"
        };

        var kiloPerCalorie = new PredictionConstant
        {
          Name = AppConstants.KiloPerCalorie,
          Value = "0.00013"
        };

        var calorieWeightFactor = new PredictionConstant
        {
          Name = AppConstants.CalorieWeightFactor,
          Value = "0.06"
        };

        //context.PredictionConstants.Add(predictionConstant);
        context.PredictionConstants.AddRange(new PredictionConstant[] { predictionConstant, kiloPerCalorie, calorieWeightFactor });
        context.SaveChanges();
      }

      //context.SaveChanges();
    }
  }
}
