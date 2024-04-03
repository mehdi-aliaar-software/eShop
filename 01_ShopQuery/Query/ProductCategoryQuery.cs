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
using DM.Infrastructure.EfCore;

namespace _01_ShopQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _context;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;

        public ProductCategoryQuery(ShopContext context, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _context = context;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
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
            var discounts = _discountContext
                .CustomerDiscounts
                .Where(x=>x.StartDate<=DateTime.Now && x.EndDate>=DateTime.Now)
                .Select(x => new {x.ProductId, x.DiscountRate }).ToList();

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

                    var specDiscount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                    if (specDiscount != null)
                    {
                        if (specDiscount.DiscountRate>0)
                        {
                            product.HasDiscount=true;
                            product.DiscountRate = specDiscount.DiscountRate;
                            if (specInventory?.unitPrice>0)
                            {
                                product.PriceAfterDiscount = Math.Round( (specInventory.unitPrice *  (100 - product.DiscountRate)/100)).ToMoney();
                            }
                            
                        }
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
