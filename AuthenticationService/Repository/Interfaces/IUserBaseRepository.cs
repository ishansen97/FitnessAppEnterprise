using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationService.Repository.Interfaces
{
  public interface IUserBaseRepository<T> where T : IdentityUser, new()
  {
    Task<T> GetByIdAsync(string id);

    Task<IdentityResult> UpdateAsync(string id, T entity);

    Task<IdentityResult> CreateUserAsync(T entity);
  }
}
