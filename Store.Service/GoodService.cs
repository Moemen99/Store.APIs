using Store.Core;
using Store.Core.Entities;
using Store.Core.Services.Contract;
using Store.Core.Specifications.Good_Specs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service
{
    public class GoodService : IGoodService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GoodService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<Good>> GetGoodsAsync(GoodSpecParams specParams)
        {
            var spec = new GoodWithStoreAndTransactionsSpecifications(specParams);
            var goods = await _unitOfWork.AccessRepository<Good>().GetAllWithSpecAsync(spec);

            return goods;
        }
        public async Task<Good?> GetGoodByIdAsync(string goodId)
        {
            var spec = new GoodWithStoreAndTransactionsSpecifications(goodId);
            var good = await _unitOfWork.AccessRepository<Good>().GetWithSpecAsync(spec);
            return good;
        }


        public async Task<IReadOnlyList<StoreInfo>> GetStoresAsync()
        =>  await _unitOfWork.AccessRepository<StoreInfo>().GetAllAsync();

        public async Task<IReadOnlyList<Transaction>> GetTransactionsAsync()
        =>  await _unitOfWork.AccessRepository<Transaction>().GetAllAsync();
    }
}
