using SM.Application.Contracts.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IM.Application.Contracts.Inventory
{
    public class CreateInventory
    {
        public long ProductId { get;  set; }
        public double unitPrice { get;  set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
