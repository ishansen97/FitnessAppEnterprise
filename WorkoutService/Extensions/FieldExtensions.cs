using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Newtonsoft.Json;

namespace WorkoutService.Extensions
{
  public static class FieldExtensions
  {
    public static string SerializeDict<TKey, TVal>(this Dictionary<TKey, TVal> dict)
    {
      var json = string.Empty;
      if (dict != null)
      {
        json = JsonConvert.SerializeObject(dict);
      }

      return json;
    }

    public static Dictionary<TKey, TVal> DeserializeDict<TKey, TVal>(this string text)
    {
      if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
      {
        return new Dictionary<TKey, TVal>();
      }

      return JsonConvert.DeserializeObject<Dictionary<TKey, TVal>>(text);
    }
  }
}
