using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Entities
{
    public class StoreInfo : BaseEntity
    {

        public string Name { get; set; }

        public DateTime StoreFileDate { get; set; }

        // Navigation property to the collection of Goods
        public virtual ICollection<Good> Goods { get; set; } = new HashSet<Good>();


    }
}
