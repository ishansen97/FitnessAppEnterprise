using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutService.Entity.Enums;

namespace WorkoutService.Entity
{
    public class Workout
    {
      public int Id { get; set; }

      public int UserId { get; set; }

      public WorkoutType WorkoutType { get; set; }

      public DateTime Created { get; set; }

      public Dictionary<string, double> Fields { get; set; }
    }
}
