﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutService.Entity.Enums;

namespace WorkoutService.Entity
{
    public class Workout : EntityBase
    {
      public WorkoutType WorkoutType { get; set; }

      public string Fields { get; set; }
    }
}
