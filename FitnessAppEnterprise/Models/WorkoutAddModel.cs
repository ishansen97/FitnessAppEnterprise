using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessAppEnterprise.Models
{
  public class WorkoutAddModel
  {
    public string UserId { get; set; }

    public List<WorkoutTypeModel> WorkoutTypes { get; set; }

    public int SelectedWorkoutType { get; set; }

    public Dictionary<string, double> Fields { get; set; }

    public DateTime Created { get; set; }
  }
}
