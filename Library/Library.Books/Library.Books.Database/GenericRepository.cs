using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Library.Books.Database.Interfaces;
using Library.Books.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Books.Database
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includes != null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetById(int id, bool asNoTracking = false)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Create(TEntity entity)
        {
            await Task.Run(async () =>
            {
                await _context.Set<TEntity>().AddAsync(entity);
                await _context.SaveChangesAsync();
            });
        }

        public async Task Update(int id, TEntity entity)
        {
            await Task.Run(async () =>
            {
                _context.Set<TEntity>().Update(entity);

                await _context.SaveChangesAsync();
            });
        }

        public async Task Delete(int id)
        {
            await Task.Run(async () =>
            {
                var entity = await GetById(id);
                _context.Set<TEntity>().Remove(entity);

                await _context.SaveChangesAsync();
            });
        }
    }
}
