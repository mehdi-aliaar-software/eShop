using _01_ShopQuery.Contracts.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _01_ShopQuery.Contracts.Product;
using Microsoft.EntityFrameworkCore;
using SM.Domain.ProductAgg;
using SM.Infrastructure.EfCore;
using IM.Infrastructure.EfCore;

namespace _01_ShopQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _context;
        private readonly InventoryContext _inventoryContext;

        public ProductCategoryQuery(ShopContext context, InventoryContext inventoryContext)
        {
            _context = context;
            _inventoryContext = inventoryContext;
        }

        public List<ProductCategoryQueryModel> GetProductCategories()
        {
            var result = _context.ProductCategories.Select(x => new ProductCategoryQueryModel
            {
                Id=x.Id,
                Picture = x.Picture,
                Name = x.Name,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            }).ToList();
            return result;
        }

        public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
        {
            var inventory = _inventoryContext.Inventory.Select(x => new { x.ProductId, x.unitPrice }).ToList();

            var categories = _context.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x=>x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Products=MappProducts(x.Products)
      
                }).ToList();

            foreach (var category in categories)
            {
                foreach (var product in category.Products)
                {
                    var specInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                    if (specInventory!=null)
                    {
                        product.Price = specInventory.unitPrice.ToMoney();
                    }
                }
            }

            return categories;
        }

        private static List<ProductQueryModel> MappProducts(List<Product> products)
        {
            var result = products.Select(x => new ProductQueryModel
            {
                Id = x.Id,
                Category = x.Category.Name,
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            }).ToList();

           return result;
        }
    }
}
