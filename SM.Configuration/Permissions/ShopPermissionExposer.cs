﻿using _0_Framework.Infrastructure;
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
                        new PermissionDto(ShopPermissions.ListProducts,"ListProducts"),
                        new PermissionDto(ShopPermissions.SearchProducts,"SearchProducts"),
                        new PermissionDto(ShopPermissions.CreateProduct,"CreateProduct"),
                        new PermissionDto(ShopPermissions.EditProduct,"EditProduct"),

                    }

                },
                {
                    "ProductCategory", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.ListProductCategories,"ListProductCategories"),
                        new PermissionDto(ShopPermissions.SearchProductCategories,"SearchProductCategories"),
                        new PermissionDto(ShopPermissions.CreateProductCategory,"CreateProductCategory"),
                        new PermissionDto(ShopPermissions.EditProductCategory,"EditProductCategory"),

                    }
                }
            };

            return result;
        }
    }
}
