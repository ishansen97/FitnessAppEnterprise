using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutService.Entity.Enums;

namespace WorkoutService.Entity
{
  public class ExerciseMeasurement
  {
    [Key]
    public int Id { get; set; }

    public WorkoutType WorkoutType { get; set; }

    public double Value { get; set; }
  }
}
