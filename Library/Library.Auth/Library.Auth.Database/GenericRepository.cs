using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Auth.Database.Interfaces;
using Library.Auth.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Auth.Database
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _context.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id == id);
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
