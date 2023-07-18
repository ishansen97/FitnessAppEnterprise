using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Entity
{
  public class Report : EntityBase
  {
    public double CalorieExpenditure { get; set; }

    public double CalorieIntake { get; set; }

    public bool IsSurplus { get; set; }
  }
}
