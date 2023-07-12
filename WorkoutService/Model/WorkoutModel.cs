using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutService.Entity;

namespace WorkoutService.Model
{
    public class WorkoutModel
    {
      public List<WorkoutTypeModel> WorkoutTypes { get; set; }

      public int SelectedWorkoutType { get; set; }

      public string UserId { get; set; }

      public Dictionary<string, double> Fields { get; set; }

      public DateTime Created { get; set; }
    }
}
