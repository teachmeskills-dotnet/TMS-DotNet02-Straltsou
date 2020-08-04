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
        /// Create new entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        void CreateEntity(T entity);

        /// <summary>
        /// Delete existing entity from the database.
        /// </summary>
        /// <param name="entity">Entity model.</param>
        void DeleteEntity(T entity);

        /// <summary>
        /// Get specific entity by ID.
        /// </summary>
        /// <param name="id">Entity id.</param>
        /// <returns>T entity.</returns>
        T GetEntityByID(int id);

        /// <summary>
        /// Get all specific entities.
        /// </summary>
        /// <returns>IEnumerable entities.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Save all changes asynchronously to database.
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();

        /// <summary>
        /// Save all changes to database.
        /// </summary>
        /// <returns></returns>
        bool SaveChanges();
    }
}
