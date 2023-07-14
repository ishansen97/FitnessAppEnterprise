using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PredictionsService.Context;
using PredictionsService.Entity;
using PredictionsService.Repository.Interfaces;

namespace PredictionsService.Repository.Implementations
{
  public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T: EntityBase, new()
  {
    private readonly PredictionDbContext _context;

    public EntityBaseRepository(PredictionDbContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
      var entities = await _context.Set<T>().ToListAsync();
      return entities;
    }

    public async Task<T> GetByIdAsync(int id)
    {
      var entity = await _context.Set<T>().FirstOrDefaultAsync(en => en.Id == id);
      return entity;
    }

    public async Task<IEnumerable<T>> GetEntitiesAsync(Expression<Func<T, bool>> expression)
    {
      var results = await _context.Set<T>().Where(expression).ToListAsync();
      return results;
    }

    public async Task AddAsync(T entity)
    {
      await _context.Set<T>().AddAsync(entity);
      await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, T entity)
    {
      EntityEntry entry = _context.Entry(entity);
      entry.State = EntityState.Modified;
      await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
      var entity = await _context.Set<T>().FirstOrDefaultAsync(en => en.Id == id);
      EntityEntry entry = _context.Entry(entity);
      entry.State = EntityState.Deleted;
      await _context.SaveChangesAsync();
    }
  }
}
