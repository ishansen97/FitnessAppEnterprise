using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutService.Entity.Enums;

namespace WorkoutService.Entity
{
    public class CheatMeal : EntityBase
    {
      public MealType MealType { get; set; }

      public double MealAmount { get; set; }
    }
}
