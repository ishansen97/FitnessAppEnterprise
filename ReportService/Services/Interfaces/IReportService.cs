using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportService.Entity;
using ReportService.Model;
using ReportService.Repository.Interfaces;

namespace ReportService.Services.Interfaces
{
  public interface IReportService : IEntityBaseRepository<Report>
  {
    Task<ReportResponseModel> CreateReportAsync(CreateReportRequestModel model);
  }
}
