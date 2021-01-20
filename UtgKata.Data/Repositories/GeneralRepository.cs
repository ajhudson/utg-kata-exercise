﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UtgKata.Data.Models;

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

        public virtual async Task<int> AddAsync(TEntity entity)
        {
            await this.dbSet.AddAsync(entity);
            await this.dbContext.SaveChangesAsync();

            return entity.Id;
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            var entities = await this.dbSet.ToListAsync();

            return entities;
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            var entity = await this.dbSet.FindAsync(id);

            return entity;
        }
    }
}
