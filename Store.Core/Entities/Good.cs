using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Entities
{
    public class Good : BaseEntity
    {
        [Key]
        public string GoodID { get; set; }
        public int GoodInitialBalance { get; set; }

        public string StoreName { get; set; }  // Foreign Key Column => StoreInfo
        public virtual StoreInfo Store { get; set; } // Navigational Property [One]


        // Navigation property to the collection of Transactions
        public virtual ICollection<Transaction> Transactions { get; set; } = new HashSet<Transaction>();
    }
}
