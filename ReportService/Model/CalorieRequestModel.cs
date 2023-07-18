using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportService.Model;

namespace ReportService.Model
{
  public class CalorieRequestModel
  {
    public List<WorkoutModel> Workouts { get; set; }

    public List<CheatMealModel> CheatMeals { get; set; }
  }
}
