using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessAppEnterprise.Helpers
{
  public class DateHelper
  {
    public string CreateWeekText(DateTime dt)
    {
      var (startOfWeekDate, endOfWeekDate) = GetStartEndWeekDates(dt);

      var startOfWeekFormatted = startOfWeekDate.ToString("yyyy MMMM dd");
      var endOfWeekFormatted = endOfWeekDate.ToString("yyyy MMMM dd");

      var startOfWeekSplitted = startOfWeekFormatted.Split(' ');
      var endOfWeekSplitted = endOfWeekFormatted.Split(' ');

      var startOfWeekMonthDate = string.Concat(startOfWeekSplitted[1], " ", startOfWeekSplitted[2]);
      var endOfWeekMonthDate = string.Concat(endOfWeekSplitted[1], " ", endOfWeekSplitted[2]);

      return string.Concat(startOfWeekMonthDate, " - ", endOfWeekMonthDate);
    }

    public (DateTime startDate, DateTime endDate) GetStartEndWeekDates(DateTime date)
    {
      var dayOfWeek = date.DayOfWeek;
      int daysAfterStartOfWeek = -((int)dayOfWeek % 7);
      int daysBeforeEndOfWeek = daysAfterStartOfWeek + 6;

      var startOfWeekDate = date.AddDays(daysAfterStartOfWeek);
      var endOfWeekDate = date.AddDays(daysBeforeEndOfWeek);

      return (startOfWeekDate, endOfWeekDate);
    }

    public List<DateTime> GetInBetweenDates(DateTime date)
    {
      var (startOfWeekDate, endOfWeekDate) = GetStartEndWeekDates(date);
      var totalDays = (endOfWeekDate - startOfWeekDate).TotalDays;
      var inBetweenDates = Enumerable.Range(1, (int)totalDays + 1)
        .Select(item => endOfWeekDate.AddDays(item - 7))
        .ToList();
      return inBetweenDates;
    }

    public string GetMonthAndDate(DateTime date)
    {
      var dateString = date.ToString("yyyy MMMM dd");
      var splitted = dateString.Split(' ');
      return string.Concat(splitted[1], " ", splitted[2]);
    }

    public string GetMonthAndDateNoGap(DateTime date)
    {
      var dateString = date.ToString("yyyy MMMM dd");
      var splitted = dateString.Split(' ');
      return string.Concat(splitted[1], splitted[2]);
    }


  }
}
