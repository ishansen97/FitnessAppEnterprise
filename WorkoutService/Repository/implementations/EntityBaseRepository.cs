using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WorkoutService.Context;
using WorkoutService.Entity;
using WorkoutService.Repository.Interfaces;

namespace WorkoutService.Repository.implementations
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T: EntityBase, new()
    {
      private readonly WorkoutDbContext _context;

      public EntityBaseRepository(WorkoutDbContext context)
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
