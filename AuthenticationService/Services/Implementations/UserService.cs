using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticationService.Data;
using AuthenticationService.Data.Models;
using AuthenticationService.Helpers;
using AuthenticationService.Model;
using AuthenticationService.Repository.Implementations;
using AuthenticationService.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationService.Services.Implementations
{
  public class UserService : UserBaseRepository<AppUser>, IUserService
  {
    private readonly ModelHelper _modelHelper;

    public UserService(
      UserManager<AppUser> userManager,
      ModelHelper modelHelper) : base(userManager)
    {
      _modelHelper = modelHelper;
    }

    public async Task<AppUserModel> GetUser(string userId)
    {
      var appUser = await GetByIdAsync(userId);
      if (appUser != null)
      {
        return _modelHelper.GetUserModelFromEntity(appUser);
      }

      return null;
    }
  }
}
