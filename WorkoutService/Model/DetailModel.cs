using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutService.Model
{
  public class DetailModel
  {
    public int Id { get; set; }

    public string UserId { get; set; }

    public int ActivityType { get; set; }

    public string Title { get; set; }

    public DateTime Created { get; set; }
  }
}
