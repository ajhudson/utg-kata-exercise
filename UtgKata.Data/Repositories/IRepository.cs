using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UtgKata.Data.Models;

namespace UtgKata.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        public Task<TEntity> GetByIdAsync(int id);

        public Task<List<TEntity>> GetAllAsync();

        public Task<int> AddAsync(TEntity entity);
    }
}
