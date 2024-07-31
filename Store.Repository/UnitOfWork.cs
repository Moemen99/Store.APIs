using Store.Core;
using Store.Core.Entities;
using Store.Core.Repositories.Contract;
using Store.Repository.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbContext;

        //private Dictionary<string, GenericRepository<BaseEntity>> _repositories;
        private Hashtable _repositories;
        public UnitOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Hashtable();
        }
        public IGenericRepository<TEntity> AccessRepository<TEntity>() where TEntity : BaseEntity
        {
            var key = typeof(TEntity).Name;

            if( !_repositories.ContainsKey(key) )
            {
                var repository = new GenericRepository<TEntity>(_dbContext);

                _repositories.Add(key, repository);
            }

            return _repositories[key] as IGenericRepository<TEntity>;
        }

        //public async Task<int> CompleteAsync()
        //=> await _dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
            => await _dbContext.DisposeAsync();
    }
}
