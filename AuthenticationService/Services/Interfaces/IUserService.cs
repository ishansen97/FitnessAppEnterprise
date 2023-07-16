using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticationService.Data.Models;
using AuthenticationService.Model;
using AuthenticationService.Repository.Interfaces;

namespace AuthenticationService.Services.Interfaces
{
  public interface IUserService : IUserBaseRepository<AppUser>
  {
    Task<AppUserModel> GetUser(string userId);
  }
}
