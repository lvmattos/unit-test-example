using Dapper;
using Dommel;
using Meetup.UnitTestExample.Domain;
using Meetup.UnitTestExample.Domain.Model;
using Meetup.UnitTestExample.Domain.Repository;
using Meetup.UnitTestExample.Domain.Util;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Meetup.UnitTestExample.Infra.DataAccess.Repository.Base
{
    public abstract class BaseRepository<TEntity>
        : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        public IUnitOfWork UnitOfWork { get; protected set; }

        public string QueryInsertList { get; set; }

        protected BaseRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await UnitOfWork.Connection().GetAllAsync<TEntity>();
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await UnitOfWork.Connection().GetAsync<TEntity>(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>The id of the inserted entity.</returns>
        public virtual async Task<object> Insert(TEntity entity)
        {
            Guard.IsNotNull(entity, nameof(entity));
            UnitOfWork.BeginTransaction();

            return await UnitOfWork.Connection()
                .InsertAsync(entity, UnitOfWork.Transaction());
        }

        public virtual async Task<int> InsertList(IEnumerable<TEntity> entityList)
        {
            Guard.IsNotNull(entityList, nameof(entityList));
            Guard.IsNotNull(QueryInsertList, nameof(QueryInsertList));
            UnitOfWork.BeginTransaction();

            return await SqlMapper.ExecuteAsync(
                UnitOfWork.Connection(), QueryInsertList,
                entityList, transaction: UnitOfWork.Transaction());
        }

        public virtual async Task<bool> Update(TEntity entity)
        {
            Guard.IsNotNull(entity, nameof(entity));
            UnitOfWork.BeginTransaction();
            return await UnitOfWork.Connection().UpdateAsync(entity, UnitOfWork.Transaction());
        }

        public virtual async Task<bool> Delete(TEntity entity)
        {
            Guard.IsNotNull(entity, nameof(entity));
            UnitOfWork.BeginTransaction();
            return await UnitOfWork.Connection().DeleteAsync(entity, UnitOfWork.Transaction());
        }

        public virtual async Task<IEnumerable<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            Guard.IsNotNull(predicate, nameof(predicate));
            return await UnitOfWork.Connection().SelectAsync(predicate);
        }

        public virtual async Task<long> Count(Expression<Func<TEntity, bool>> predicate)
        {
            Guard.IsNotNull(predicate, nameof(predicate));
            return await UnitOfWork.Connection().CountAsync(predicate);
        }

        public virtual void Dispose()
        {
            UnitOfWork.Dispose();
            GC.SuppressFinalize(this);
        }

        public virtual async Task<TEntity> GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            Guard.IsNotNull(predicate, nameof(predicate));
            return await UnitOfWork.Connection().FirstOrDefaultAsync(predicate);
        }
    }
}
