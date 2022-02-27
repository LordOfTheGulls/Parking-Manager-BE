using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : TableEntity
    {
        public Task<TEntity> GetById(long id);
        public Task<IEnumerable<TEntity>> GetAll();
        public Task<long> GetCount();
        public Task Add(TEntity entity);
        public Task AddRange(IEnumerable<TEntity> entities);
        public void Update(TEntity entity);
        public void UpdateRange(IEnumerable<TEntity> entity);
        public void Remove(TEntity entity);
        public void RemoveRange(IEnumerable<TEntity> entities);
        public Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
    }
}