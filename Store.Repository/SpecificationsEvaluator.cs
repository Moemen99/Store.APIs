using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Store.Core.Entities;
using Store.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository
{
    internal static class SpecificationsEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity> spec)
        {
            var query = inputQuery; // _dbContext.Set<Good>()

            if(spec.Criteria is not null) // G => G.GoodID == 5401
            query = query.Where(spec.Criteria);
            // query = _dbContext.Set<Good>().Where(G => G.GoodID == 5401)
            // Includes
            // 1. G => G.Store
            // 2. G => G.Transactions

            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            // query = _dbContext.Set<Good>().Where(G => G.GoodID == 5401).Include(G => G.Store);
            // query = _dbContext.Set<Good>().Where(G => G.GoodID == 5401).Include(G => G.Store).Include(G => G.Transactions);

            return query;
        }

    }
}
