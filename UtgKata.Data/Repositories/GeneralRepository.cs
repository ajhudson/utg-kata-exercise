﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UtgKata.Data.Models;
using System.Linq.Expressions;

namespace UtgKata.Data.Repositories
{
    public class GeneralRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly UtgKataDbContext dbContext;

        protected readonly DbSet<TEntity> dbSet;

        public GeneralRepository(UtgKataDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }

        /// <summary>
        /// Add a new entity to the database/
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Get all records from the database of a specific type
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            var entities = await this.dbSet.ToListAsync();

            return entities;
        }

        /// <summary>
        /// Get a specific record using the primary key id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            var entity = await this.dbSet.FindAsync(id);

            return entity;
        }

        /// <summary>
        /// Get the first match using the criteria supplied.
        /// </summary>
        /// <param name="filterCriteria"></param>
        /// <returns></returns>
        public async Task<TEntity> GetFirstMatchAsync(Func<TEntity, bool> filterCriteria)
        {
            var entity = this.dbSet.Where(filterCriteria).FirstOrDefault();

            return await Task.FromResult(entity);
        }
    }
}
