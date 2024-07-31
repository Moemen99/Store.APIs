using Microsoft.EntityFrameworkCore;
using Store.Core.Entities;
using Store.Core.Repositories.Contract;
using Store.Core.Specifications;
using Store.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext)
        {
           _dbContext = dbContext;
        }
        public async Task<IReadOnlyList<T>>  GetAllAsync()
        {
            //if (typeof(T) == typeof(Good))
            //    return (IEnumerable < T >) await _dbContext.Set<Good>().Include(G => G.Store).ToListAsync();
          return  await _dbContext.Set<T>().ToListAsync();
        }


        public async Task<T?> GetAsync(string id)
        {
            //if (typeof(T) == typeof(Good))
            //    return await _dbContext.Set<Good>().Where(G => G.GoodID == id).Include(G => G.Store).FirstOrDefaultAsync() as T;
           return  await _dbContext.Set<T>().FindAsync(id);
        }


        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecifications(spec).ToListAsync();
        }

        public async Task<T?> GetWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }

        private IQueryable<T> ApplySpecifications(ISpecifications<T> spec)
        {
            return SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec);
        }
    }
}
