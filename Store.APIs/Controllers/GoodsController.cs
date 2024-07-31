using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.APIs.DTOs;
using Store.APIs.Errors;
using Store.Core.Entities;
using Store.Core.Repositories.Contract;
using Store.Core.Services.Contract;
using Store.Core.Specifications.Good_Specs;

namespace Store.APIs.Controllers
{
    public class GoodsController : BaseApiController
    {
        //private readonly IGenericRepository<Good> _goodsRepo;
        //private readonly IGenericRepository<StoreInfo> _storesRepo;
        //private readonly IGenericRepository<Transaction> _transactionsRepo;
        private readonly IGoodService _goodService;
        private readonly IMapper _mapper;

        public GoodsController(
            IGoodService goodService,
            //IGenericRepository<Good> goodsRepo,
            //IGenericRepository<StoreInfo> storesRepo,
            //IGenericRepository<Transaction> transactionsRepo,
            IMapper mapper)
        {
            //_goodsRepo = goodsRepo;
            //_storesRepo = storesRepo;
            //_transactionsRepo = transactionsRepo;
            _goodService = goodService;
            _mapper = mapper;
        }

        [HttpGet] // /api/Goods
        public async Task<ActionResult<IReadOnlyList<GoodToReturnDTO>>> GetGoods( [FromQuery] GoodSpecParams specParams)
        {
            var goods = await _goodService.GetGoodsAsync(specParams);

            return Ok(_mapper.Map<IReadOnlyList<Good>, IReadOnlyList<GoodToReturnDTO>>(goods));
        }

        [ProducesResponseType(typeof(GoodToReturnDTO),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        [HttpGet("id")] // /api/Good/id?id=5401
        public async Task<ActionResult<GoodToReturnDTO>> GetById(string id)
        {
            
            var good = await _goodService.GetGoodByIdAsync(id);

            if (good is null)
                return NotFound(new ApiResponse(404)); //404

            return Ok(_mapper.Map<Good, GoodToReturnDTO>(good)); //200
        }


        [HttpGet("stores")] // Get: /api/goods/stores
        public async Task<ActionResult<IReadOnlyList<StoreToReturnDTO>>> GetStores()
        {
            var stores = await _goodService.GetStoresAsync();
            return Ok(_mapper.Map<IReadOnlyList<StoreInfo>, IReadOnlyList<StoreToReturnDTO>>(stores));
        }

        [HttpGet("transactions")] // Get: /api/goods/transactions
        public async Task<ActionResult<IReadOnlyList<TransactionToReturnDTO>>> GetTransactions()
        {
            var transactions = await _goodService.GetTransactionsAsync();
            return Ok(_mapper.Map<IReadOnlyList<Transaction>, IReadOnlyList<TransactionToReturnDTO>>(transactions));
        }

    }
}
