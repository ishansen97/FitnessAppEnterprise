using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutService.Entity.Enums;

namespace WorkoutService.Model
{
  public class MealMeasurementModel
  {
    public int Id { get; set; }

    public string MealType { get; set; }

    public double Value { get; set; }
  }
}
