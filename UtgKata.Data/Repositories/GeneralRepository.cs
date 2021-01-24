// <copyright file="GeneralRepository.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using UtgKata.Data.Models;

    /// <summary>
    /// Generic repository for any class which inherits from <see cref="BaseEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="UtgKata.Data.Repositories.IRepository{TEntity}" />
    public class GeneralRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        /// <summary>The database context.</summary>
        private readonly UtgKataDbContext dbContext;

        /// <summary>The database set.</summary>
        private readonly DbSet<TEntity> dbSet;

        /// <summary>Initializes a new instance of the <see cref="GeneralRepository{TEntity}" /> class.</summary>
        /// <param name="dbContext">The database context.</param>
        public GeneralRepository(UtgKataDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }

        /// <summary>Adds the asynchronous.</summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            // if the entity implements ICreatedAt then we want to set the created at property
            var createdAtEntity = entity as IEntityCreatedAt;

            if (createdAtEntity != null)
            {
                createdAtEntity.CreatedAt = DateTime.Now;
            }

            await this.dbSet.AddAsync(entity);
            await this.dbContext.SaveChangesAsync();

            return entity;
        }

        /// <summary>Gets all asynchronous.</summary>
        /// <returns>
        ///   List of entities.
        /// </returns>
        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            var entities = await this.dbSet.ToListAsync();

            return entities;
        }

        /// <summary>Gets the by identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            var entity = await this.dbSet.FindAsync(id);

            return entity;
        }

        /// <summary>Gets the first match asynchronous.</summary>
        /// <param name="filterCriteria">The filter criteria.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public async Task<TEntity> GetFirstMatchAsync(Func<TEntity, bool> filterCriteria)
        {
            var entity = this.dbSet.Where(filterCriteria).FirstOrDefault();

            return await Task.FromResult(entity);
        }
    }
}
