using Store.Core.Entities;
using Store.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core
{
    public interface IUnitOfWork : IAsyncDisposable 
    {
        public IGenericRepository<TEntity> AccessRepository<TEntity>() where TEntity : BaseEntity;


        //Task<int> CompleteAsync();


    }
}
