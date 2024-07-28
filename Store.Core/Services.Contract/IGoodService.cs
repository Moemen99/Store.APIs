using Store.Core.Entities;
using Store.Core.Specifications.Good_Specs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Services.Contract
{
    public interface IGoodService
    {
        Task<IReadOnlyList<Good>> GetGoodsAsync(GoodSpecParams specParams);

        Task<Good?> GetGoodByIdAsync(string goodId);

        Task<IReadOnlyList<StoreInfo>> GetStoresAsync();

        Task<IReadOnlyList<Transaction>> GetTransactionsAsync();
    }
}
