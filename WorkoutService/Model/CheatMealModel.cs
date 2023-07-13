using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutService.Model
{
  public class CheatMealModel
  {
    public int Id { get; set; } 

    public List<CheatMealTypeModel> CheatMealTypes { get; set; }

    public int SelectedCheatMealType { get; set; }

    public string UserId { get; set; }

    public double MealAmount { get; set; }

    public DateTime Created { get; set; }
  }
}
