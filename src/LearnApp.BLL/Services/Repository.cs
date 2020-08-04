using LearnApp.Common.Interfaces;
using LearnApp.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnApp.BLL.Services
{
    /// <summary>
    /// Repository class which allows to manage with entities.
    /// </summary>
    /// <typeparam name="T">Class of entity.</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        /// <summary>
        /// Constructor which resolves an application DB context.
        /// </summary>
        /// <param name="context">Current context.</param>
        public Repository(ApplicationDbContext context)
        {
            _dbSet = context.Set<T>();
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public void CreateEntity(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _dbSet.Add(entity);
        }

        /// <inheritdoc/>
        public void DeleteEntity(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _dbSet.Remove(entity);
        }

        /// <inheritdoc/>
        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        /// <inheritdoc/>
        public T GetEntityByID(int id)
        {
            return _dbSet.Find(id);
        }

        /// <inheritdoc/>
        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
