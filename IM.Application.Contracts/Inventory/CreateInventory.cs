using SM.Application.Contracts.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace IM.Application.Contracts.Inventory
{
    public class CreateInventory
    {
        [Range(1,100000,ErrorMessage = ValidationMessages.IsRequired)]
        public long ProductId { get;  set; }

        [Range(1, double.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]

        public double unitPrice { get;  set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
