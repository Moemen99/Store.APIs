using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Specifications.Good_Specs
{
    public class GoodWithStoreAndTransactionsSpecifications : BaseSpecifications<Good>
    {
        // This constructor will be used for creating object, That will be used to Get All Goods
        public GoodWithStoreAndTransactionsSpecifications(GoodSpecParams specParams) 
            : base(G=> (string.IsNullOrEmpty(specParams.GoodId) || G.GoodID == specParams.GoodId) )
        {
            Includes.Add(G => G.Store);
            Includes.Add(G => G.Transactions.
            Where(T => 
            (!specParams.TransactionStartDate.HasValue || T.TransactionDate.Date >= specParams.TransactionStartDate.Value.Date) &&
            (!specParams.TransactionEndDate.HasValue || T.TransactionDate.Date <= specParams.TransactionEndDate.Value.Date)
            ));
            // Logging for debugging
            Console.WriteLine($"GoodId: {specParams.GoodId}");
            Console.WriteLine($"StartDate: {specParams.TransactionStartDate}");
            Console.WriteLine($"EndDate: {specParams.TransactionEndDate}");
        }

        // This constructor will bed used for creating object, That will be used to Get Good with specific id
        public GoodWithStoreAndTransactionsSpecifications(string id):base(G => G.GoodID == id)
        {
            Includes.Add(G => G.Store);
            Includes.Add(G => G.Transactions);

        }
      
    }
}
