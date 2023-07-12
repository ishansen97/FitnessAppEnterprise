using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FitnessAppEnterprise.Models;
using FitnessAppEnterprise.Models.Enums;

namespace FitnessAppEnterprise.Helpers
{
  public class ModelHelper
  {
    public string GetEndpoint(EndpointType endpoint)
    {
      string endpointUrl = string.Empty;
      switch (endpoint)
      {
        case EndpointType.Workout:
          endpointUrl = "workout";
          break;
        case EndpointType.WorkoutTypes:
          endpointUrl = "workouttypes";
          break;
        case EndpointType.Predictions:
          endpointUrl = "prediction";
          break;
        case EndpointType.Reports:
          endpointUrl = "report";
          break;
        case EndpointType.WeeklyViews:
          endpointUrl = "weeklyview";
          break;
        case EndpointType.CheatMeals:
          endpointUrl = "cheatmeal";
          break;
        case EndpointType.CheatMealTypes:
          endpointUrl = "cheatmealtypes";
          break;

      }

      return endpointUrl;
    }

    public string GetResourceByHttpMethod(HttpMethod httpMethod)
    {
      string resource = string.Empty;
      if (httpMethod == HttpMethod.Get)
      {
        resource = "";
      }
      else if (httpMethod == HttpMethod.Post)
      {
        resource = "add";
      }
      else if (httpMethod == HttpMethod.Put)
      {
        resource = "update";
      }
      else if (httpMethod == HttpMethod.Delete)
      {
        resource = "delete";
      }

      return resource;
    }
  }
}
