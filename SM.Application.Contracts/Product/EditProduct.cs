using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Application.Contracts.Product
{
    public class EditProduct:CreateProduct
    {
        public long Id { get; set; }
    }
}
