using _0_Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Configuration.Permissions
{
    public class ShopPermissionExposer: IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            var result= new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Product", new List<PermissionDto>
                    {
                        new PermissionDto(10,"ListProducts"),
                        new PermissionDto(11,"SearchProducts"),
                        new PermissionDto(12,"CreateProduct"),
                        new PermissionDto(13,"EditProduct"),

                    }

                },
                {
                    "ProductCategory", new List<PermissionDto>
                    {
                        new PermissionDto(20,"ListProductCategories"),
                        new PermissionDto(21,"SearchProductCategories"),
                        new PermissionDto(22,"CreateProductCategory"),
                        new PermissionDto(23,"EditProductCategory"),

                    }
                }
            };

            return result;
        }
    }
}
