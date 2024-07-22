using Store.Core.Entities;
using Store.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Repositories.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
          Task<IReadOnlyList<T>>GetAllAsync();

          Task<T?> GetAsync(string id);

          Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec);

          Task<T?> GetWithSpecAsync(ISpecifications<T> spec);

    }
}
