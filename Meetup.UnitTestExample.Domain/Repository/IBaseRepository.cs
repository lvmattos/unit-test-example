using Meetup.UnitTestExample.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Meetup.UnitTestExample.Domain.Repository
{
    public interface IBaseRepository<TEntity> :
        IDisposable where TEntity : BaseEntity
    {
        Task<object> Insert(TEntity entity);
        Task<int> InsertList(IEnumerable<TEntity> entityList);
        Task<TEntity> GetById(int id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task<bool> Update(TEntity entity);
        Task<bool> Delete(TEntity entity);
        Task<long> Count(Expression<Func<TEntity, bool>> predicate);
    }
}
