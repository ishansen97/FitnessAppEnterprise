using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessAppEnterprise.Models
{
  public class DailyDetailModel
  {
    public DateTime Created { get; set; }

    public List<WorkoutEditModel> Workouts { get; set; }

    public List<CheatMealEditModel> CheatMeals { get; set; }
  }
}
