using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessAppEnterprise.Models
{
  public class CheatMealEditModel
  {
    public int Id { get; set; }

    public string UserId { get; set; }

    public string MealType { get; set; }

    public DateTime Created { get; set; }

    public double MealAmount { get; set; }
  }
}
