using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeeklyViewService.Model
{
  public class DailyDetail
  {
    public DateTime Created { get; set; }

    public List<WorkoutModel> Workouts { get; set; }

    public List<CheatMealModel> CheatMeals { get; set; }
  }
}
