using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PredictionsService.Helpers;
using ReportService.Context;
using ReportService.Entity;
using ReportService.Model;
using ReportService.Model.Enums;
using ReportService.Repository.Implementations;
using ReportService.Services.Interfaces;

namespace ReportService.Services
{
  public class ReportHandler : EntityBaseRepository<Report>, IReportService
  {
    private readonly ModelHelper _modelHelper;
    private readonly IRemoteService _remoteService;

    public ReportHandler(
      AppDbContext context,
      ModelHelper modelHelper,
      IRemoteService remoteService) : base(context)
    {
      _modelHelper = modelHelper;
      _remoteService = remoteService;
    }

    public async Task<ReportResponseModel> CreateReportAsync(CreateReportRequestModel model)
    {
      var calorieRequestModel = new CalorieRequestModel()
      {
        Workouts = model.Workouts,
        CheatMeals = model.CheatMeals
      };

      var calorieResponse =
        await _remoteService.PostDataWithSpecialParamsAsync<CalorieRequestModel, CalorieResponseModel>(
          EndpointType.Predictions, calorieRequestModel, "calorie", model.UserId);

      // check for existing daily report
      Report report = null;
      var dailyReports = await GetEntitiesAsync(report => report.Created == model.Created);
      if (dailyReports.Any())
      {
        report = dailyReports.ToList().First();
      }

      if (calorieResponse != null)
      {
        report = new Report()
        {
          CalorieExpenditure = calorieResponse.CalorieExpenditure,
          CalorieIntake = calorieResponse.CalorieIntake,
          IsSurplus = calorieResponse.IsSurplus,
          Created = model.Created
        };

        report = await AddAsync(report);
      }

      return _modelHelper.GetReportModelFromReport(report);
    }
  }
}
