using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Model
{
  public class ReportResponseModel
  {
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public double CalorieExpenditure { get; set; }

    public double CalorieIntake { get; set; }

    public double Gap => CalorieIntake - CalorieExpenditure;

    public bool IsSurplus { get; set; }
  }
}
