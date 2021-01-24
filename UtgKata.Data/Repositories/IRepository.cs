// <copyright file="IRepository.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UtgKata.Data.Models;

    /// <summary>
    /// Interface for a generic DB repository.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IRepository<TEntity>
        where TEntity : BaseEntity
    {
        /// <summary>Gets the by identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Task<TEntity> GetByIdAsync(int id);

        /// <summary>Gets all asynchronous.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public Task<List<TEntity>> GetAllAsync();

        /// <summary>Adds the asynchronous.</summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Task<TEntity> AddAsync(TEntity entity);

        /// <summary>Gets the first match asynchronous.</summary>
        /// <param name="filterCriteria">The filter criteria.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Task<TEntity> GetFirstMatchAsync(Func<TEntity, bool> filterCriteria);
    }
}
