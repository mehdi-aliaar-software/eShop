using _0_Framework.Application;
using _01_ShopQuery.Contracts.Product;
using DM.Infrastructure.EfCore;
using IM.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using SM.Infrastructure.EfCore;

namespace _01_ShopQuery.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _context;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;

        public ProductQuery(ShopContext context, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _context = context;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }


        public List<ProductQueryModel> GetlatestArrivals()
        {
            var inventory = _inventoryContext.Inventory.Select(x => new { x.ProductId, x.unitPrice }).ToList();
            var discounts = _discountContext
                .CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate }).ToList();

            var products = _context
                .Products
                .Include(x => x.Category)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Category = x.Category.Name,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug
                }).OrderByDescending(x=>x.Id).Take(6).ToList();


            foreach (var product in products)
            {
                var specInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                if (specInventory != null)
                {
                    product.Price = specInventory.unitPrice.ToMoney();
                }

                var specDiscount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                if (specDiscount != null)
                {
                    if (specDiscount.DiscountRate > 0)
                    {
                        product.HasDiscount = true;
                        product.DiscountRate = specDiscount.DiscountRate;
                        if (specInventory?.unitPrice > 0)
                        {
                            product.PriceAfterDiscount = Math.Round((specInventory.unitPrice * (100 - product.DiscountRate) / 100)).ToMoney();
                        }

                    }
                }
            }



            return products;
        }
    }
}
