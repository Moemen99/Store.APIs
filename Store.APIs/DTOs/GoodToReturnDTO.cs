using Store.Core.Entities;

namespace Store.APIs.DTOs
{
    public class GoodToReturnDTO
    {
        public string GoodID { get; set; }
        public int GoodInitialBalance { get; set; }
        public  StoreToReturnDTO Store { get; set; }

        public ICollection<TransactionToReturnDTO> Transactions { get; set; }
    }
}
