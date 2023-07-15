using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionsService.Entity.Enum
{
  public enum PredictedWeightStatus
  {
    None = 0,
    Underweight = 1,
    Normal = 2,
    Overweight = 3,
    Obese = 4,
    ExtremelyObese = 5,
  }
}
