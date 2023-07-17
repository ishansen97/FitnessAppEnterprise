using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeklyViewService.Model;

namespace WeeklyViewService.Services.Interfaces
{
  public interface IWeeklyViewService
  {
    Task<bool> WeeklyDetailsAvailable(WeeklyViewRequestModel request);

    Task<List<DailyDetail>> GetWeeklyDetails(WeeklyViewRequestModel request);
  }
}
