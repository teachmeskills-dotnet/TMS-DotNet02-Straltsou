using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearnApp.Common.Interfaces
{
    /// <summary>
    /// Interface which allows to operate with entities.
    /// </summary>
    /// <typeparam name="T">Class of entity.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Save all changes to database.
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();

        /// <summary>
        /// Create new entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        void CreateEntity(T entity);

        /// <summary>
        /// Get all specific entities.
        /// </summary>
        /// <returns>IEnumerable entities.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Get specific entity by ID.
        /// </summary>
        /// <param name="id">Entity id.</param>
        /// <returns>T entity.</returns>
        T GetEntityByID(int id);
    }
}
