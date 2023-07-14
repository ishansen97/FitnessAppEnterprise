using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PredictionsService.Entity;

namespace PredictionsService.Repository.Interfaces
{
  public interface IEntityBaseRepository<T> where T : EntityBase, new()
  {
    Task<IEnumerable<T>> GetAllAsync();

    Task<T> GetByIdAsync(int id);

    Task<IEnumerable<T>> GetEntitiesAsync(Expression<Func<T, bool>> expression);

    Task AddAsync(T entity);

    Task UpdateAsync(int id, T entity);

    Task DeleteAsync(int id);
  }
}
