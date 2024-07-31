using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Entities
{
    public class Transaction : BaseEntity
    {
        public string TransactionID { get; set; }

        public DateTime TransactionDate { get; set; }

        public int Amount { get; set; }

        public string Direction { get; set; }

        public string? Comment  {get; set; }

        // Foreign key
        public string GoodID { get; set; }
        public virtual Good Good { get; set; }
    }
}
