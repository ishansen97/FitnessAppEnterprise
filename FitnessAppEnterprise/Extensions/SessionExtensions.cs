using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace FitnessAppEnterprise.Extensions
{
  public static class SessionExtensions
  {
    public static void SetObject<T>(this ISession session, string key, T obj)
    {
      session.SetString(key, JsonConvert.SerializeObject(obj));
    }

    public static T GetObject<T>(this ISession session, string key)
    {
      var objString = session.GetString(key);
      if (!string.IsNullOrEmpty(objString))
      {
        return JsonConvert.DeserializeObject<T>(objString);
      }

      return default(T);
    }
  }
}
