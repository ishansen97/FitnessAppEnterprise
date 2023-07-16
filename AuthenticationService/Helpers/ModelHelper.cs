using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticationService.Data.Models;
using AuthenticationService.Model;

namespace AuthenticationService.Helpers
{
  public class ModelHelper
  {
    public AppUserModel GetUserModelFromEntity(AppUser appUser)
    {
      var userModel = new AppUserModel
      {
        UserId = appUser.Id,
        FirstName = appUser.FirstName,
        LastName = appUser.LastName,
        Age = appUser.Age,
        Height = appUser.Height,
        Weight = appUser.Weight
      };

      return userModel;
    }
  }
}
