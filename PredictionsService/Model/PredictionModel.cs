using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PredictionsService.Entity.Enum;

namespace PredictionsService.Model
{
  public class PredictionModel
  {
    public string UserId { get; set; }

    public string State { get; set; }

    public double CurrentWeight { get; set; }

    public double PredictedWeight { get; set; }

    public double BodyFatPercentage { get; set; }

    public DateTime PredictedDate { get; set; }

    public double BMI { get; set; }

    public double PredictedBMI { get; set; }

    public string WeightStatus { get; set; }

    public string Message { get; set; }
  }
}
