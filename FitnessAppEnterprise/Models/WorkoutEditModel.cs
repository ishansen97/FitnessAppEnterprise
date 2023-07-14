using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessAppEnterprise.Models
{
  public class WorkoutEditModel
  {
    public int Id { get; set; }

    public string UserId { get; set; }

    public string WorkoutType { get; set; }

    public DateTime Created { get; set; }

    public Dictionary<string, double> Fields { get; set; }
  }
}
