using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthenticationService.Data;
using AuthenticationService.Data.Models;
using AuthenticationService.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AuthenticationService.Repository.Implementations
{
  public class UserBaseRepository<T> : IUserBaseRepository<T> where T : IdentityUser, new()
  {
    private readonly UserManager<T> _userManager;

    public UserBaseRepository(UserManager<T> userManager)
    {
      _userManager = userManager;
    }

    public async Task<T> GetByIdAsync(string id)
    {
      var user = await _userManager.FindByIdAsync(id);
      return user;
    }

    public async Task<IdentityResult> CreateUserAsync(T entity)
    {
      var result = await _userManager.CreateAsync(entity);
      return result;
    }

    public async Task<IdentityResult> UpdateAsync(string id, T entity)
    {
      var user = await GetByIdAsync(id);
      var result = await _userManager.UpdateAsync(user);
      return result;
    }
  }
}
