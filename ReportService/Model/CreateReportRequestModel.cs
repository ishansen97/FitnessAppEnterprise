using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Model
{
  public class CreateReportRequestModel
  {
    public int Id { get; set; }

    public string UserId { get; set; }

    public DateTime Created { get; set; }

    public List<WorkoutModel> Workouts { get; set; }

    public List<CheatMealModel> CheatMeals { get; set; }
  }
}
