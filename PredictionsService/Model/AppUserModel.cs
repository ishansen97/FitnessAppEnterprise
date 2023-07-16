using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionsService.Model
{
  public class AppUserModel
  {
    public string UserId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public int Age { get; set; }

    public int Height { get; set; }

    public double Weight { get; set; }
  }
}
