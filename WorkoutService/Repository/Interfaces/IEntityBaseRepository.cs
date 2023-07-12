using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutService.Entity;

namespace WorkoutService.Repository.Interfaces
{
  public interface IEntityBaseRepository<T> where T : EntityBase, new()
  {
    Task<IEnumerable<T>> GetAllAsync();

    Task<T> GetByIdAsync(int id);

    Task AddAsync(T entity);

    Task UpdateAsync(int id, T entity);

    Task DeleteAsync(int id);
  }
}
