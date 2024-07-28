using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Specifications.Good_Specs
{
    public class GoodSpecParams
    {
        public string? GoodId { get; set; }

        public DateTime? TransactionStartDate { get; set; }
        public DateTime? TransactionEndDate { get; set; }



    }
}
