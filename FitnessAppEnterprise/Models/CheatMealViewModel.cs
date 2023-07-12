using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessAppEnterprise.Models
{
  public class CheatMealViewModel
  {
    public string UserId { get; set; }

    public List<CheatMealTypeModel> CheatMealTypes { get; set; }

    public int SelectedCheatMealType { get; set; }

    public double MealAmount { get; set; }

    public DateTime Created { get; set; }
  }
}
