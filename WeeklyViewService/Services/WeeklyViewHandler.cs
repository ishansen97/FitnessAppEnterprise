using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeklyViewService.Helpers;
using WeeklyViewService.Model;
using WeeklyViewService.Model.Enums;
using WeeklyViewService.Services.Interfaces;

namespace WeeklyViewService.Services
{
  public class WeeklyViewHandler : IWeeklyViewService
  {
    private readonly IRemoteService _remoteService;
    private readonly DateHelper _dateHelper;

    public WeeklyViewHandler(
      DateHelper dateHelper,
      IRemoteService remoteService)
    {
      _dateHelper = dateHelper;
      _remoteService = remoteService;
    }

    public async Task<bool> WeeklyDetailsAvailable(WeeklyViewRequestModel request)
    {
      var date = request.Date;
      var userId = request.UserId;
      var (startDate, endDate) = _dateHelper.GetStartEndWeekDates(date);
      var accessModel = new ActivityAccessModel()
      {
        StartDate = startDate,
        EndDate = endDate
      };
      var weeklyWorkouts = await
        _remoteService.PostDataWithSpecialParamsAsync<ActivityAccessModel, List<WorkoutModel>>(EndpointType.Workout,
          accessModel, "weeklyview", userId);
      var weeklyCheatMeals = await
        _remoteService.PostDataWithSpecialParamsAsync<ActivityAccessModel, List<CheatMealModel>>(EndpointType.CheatMeals,
          accessModel, "weeklyview", userId);

      return (weeklyWorkouts.Any() || weeklyCheatMeals.Any());
    }

    public async Task<List<DailyDetail>> GetWeeklyDetails(WeeklyViewRequestModel request)
    {
      var requestDate = request.Date;
      var userId = request.UserId;
      var (startDate, endDate) = _dateHelper.GetStartEndWeekDates(requestDate);
      var accessModel = new ActivityAccessModel()
      {
        StartDate = startDate,
        EndDate = endDate
      };
      var inBetweenDates = _dateHelper.GetInBetweenDates(requestDate);
      var weeklyWorkouts = await 
        _remoteService.PostDataWithSpecialParamsAsync<ActivityAccessModel, List<WorkoutModel>>(EndpointType.Workout,
          accessModel, "weeklyview", userId);
      var weeklyCheatMeals = await
        _remoteService.PostDataWithSpecialParamsAsync<ActivityAccessModel, List<CheatMealModel>>(EndpointType.CheatMeals,
          accessModel, "weeklyview", userId);

      var weeklyWorkoutGroup = weeklyWorkouts.GroupBy(wk => wk.Created).ToList();
      var weeklyCheatMeaLlGroupList = weeklyCheatMeals.GroupBy(cm => cm.Created).ToList();
      var weeklyDetails = new List<DailyDetail>();
      var workoutDict = GetDateTimeDictForWorkout(weeklyWorkoutGroup, weeklyWorkouts);
      var cheatMealDict = GetDateTimeDictForCheatMeal(weeklyCheatMeaLlGroupList, weeklyCheatMeals);

      foreach (var date in inBetweenDates)
      {
        var dailyDetail = new DailyDetail
        {
          Created = date,
          Workouts = GetItemsFromDict(workoutDict, date),
          CheatMeals = GetItemsFromDict(cheatMealDict, date)
        };
        weeklyDetails.Add(dailyDetail);
      }
      return weeklyDetails;
    }

    private Dictionary<DateTime, List<WorkoutModel>> GetDateTimeDictForWorkout(List<IGrouping<DateTime, WorkoutModel>> groups, List<WorkoutModel> items)
    {
      var dict = new Dictionary<DateTime, List<WorkoutModel>>();

      foreach (var group in groups)
      {
        var list = items.Where(item => item.Created == group.Key).ToList();
        dict.Add(group.Key, list);
      }

      return dict;
    }

    private Dictionary<DateTime, List<CheatMealModel>> GetDateTimeDictForCheatMeal(List<IGrouping<DateTime, CheatMealModel>> groups,
                                                                                  List<CheatMealModel> items)
    {
      var dict = new Dictionary<DateTime, List<CheatMealModel>>();

      foreach (var group in groups)
      {
        var list = items.Where(item => item.Created == group.Key).ToList();
        dict.Add(group.Key, list);
      }

      return dict;
    }



    private List<T> GetItemsFromDict<T>(Dictionary<DateTime, List<T>> dict, DateTime key)
    {
      if (dict.ContainsKey(key))
      {
        return dict[key];
      }

      return new List<T>();
    }
  }
}
