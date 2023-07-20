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

    public AppUser GetAppUserFromModel(AppUserModel model)
    {
      var user = new AppUser
      {
        Id = model.UserId,
        FirstName = model.FirstName,
        LastName = model.LastName,
        Age = model.Age,
        Height = model.Height,
        Weight = model.Weight
      };

      return user;
    }

    public void PopulateAppUserFromModel(AppUserModel model, AppUser user)
    {
      user.FirstName = model.FirstName;
      user.LastName = model.LastName;
      user.Age = model.Age;
      user.Height = model.Height;
      user.Weight = model.Weight;
    }
  }
}
