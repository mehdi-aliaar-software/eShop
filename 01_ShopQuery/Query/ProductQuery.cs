using _0_Framework.Application;
using _01_ShopQuery.Contracts.Product;
using CM.Infrastructure.EfCore;
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
        private readonly CommentContext _commentCotext;

        public ProductQuery(ShopContext context, InventoryContext inventoryContext, 
            DiscountContext discountContext, CommentContext commentCotext)
        {
            _context = context;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
            _commentCotext = commentCotext;
        }


        public ProductQueryModel GetProductDtails(string slug)
        {
            var _inventory = _inventoryContext.Inventory.Select(x => new { x.Id,x.ProductId, x.unitPrice, x.InStock }).ToList();

            var discounts = _discountContext
                .CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate }).ToList();

            var product = _context
                .Products
                .Include(x => x.Category)
                .Include(x=>x.ProductPictures)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Category = x.Category.Name,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    CategorySlug = x.Category.Slug,
                    Code=x.Code,
                    ShortDescription = x.ShortDescription,
                    Description = x.Description,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    
                }).FirstOrDefault(x => x.Slug == slug);
            if (product == null)
            {
                return new ProductQueryModel();
            }


            var productInventory = _inventory.FirstOrDefault(x => x.Id == product.Id);
            if (productInventory != null)
            {
                product.IsInStock = productInventory.InStock;
                var price = productInventory.unitPrice;
                product.Price = price.ToMoney();

                var productDiscount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                if (productDiscount != null)
                {
                    if (productDiscount.DiscountRate > 0)
                    {
                        product.DiscountExpireDate = productDiscount .EndDate.ToDiscountFormat();
                        product.HasDiscount = true;
                        product.DiscountRate = productDiscount.DiscountRate;
                        if (productInventory?.unitPrice > 0)
                        {
                            product.PriceAfterDiscount =
                                Math.Round((productInventory.unitPrice * (100 - product.DiscountRate) / 100)).ToMoney();
                        }
                    }
                }
            }

            product.Comments = _commentCotext
                .Comments
                .Where(x => x.Type == CommentType.Product)
                .Where(x=>x.OwnerRecordId==product.Id)
                .Where(x => !x.IsCanceled && x.IsConfirmed)
                .Select(x=>new CommentQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Message = x.Message,
                })
                .OrderByDescending(x => x.Id)
                .ToList();

            return product;
        }



        public List<ProductQueryModel> GetLatestArrivals()
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
                }).OrderByDescending(x => x.Id).Take(6).ToList();


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
                            product.PriceAfterDiscount =
                                Math.Round((specInventory.unitPrice * (100 - product.DiscountRate) / 100)).ToMoney();
                        }

                    }
                }
            }



            return products;
        }

        public List<ProductQueryModel> Search(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            var inventory = _inventoryContext.Inventory.Select(x => new { x.ProductId, x.unitPrice }).ToList();
            var discounts = _discountContext
                .CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate }).ToList();

            var query = _context.Products
                .Include(x => x.Category)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Category = x.Category.Name,
                    CategorySlug = x.Category.Slug,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    ShortDescription = x.ShortDescription,
                    Slug = x.Slug
                }).AsNoTracking();
            query = query.Where(x => x.Name.Contains(value) || x.ShortDescription.Contains(value));
            var products = query.OrderByDescending(x => x.Id).ToList();

            foreach (var product in products)
            {
                var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                if (productInventory != null)
                {
                    var price = productInventory.unitPrice;
                    product.Price = price.ToMoney();

                    var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                    if (discount != null)
                    {
                        var discountRate = discount.DiscountRate;
                        product.DiscountRate = discountRate;
                        product.HasDiscount = discountRate > 0;
                        product.DiscountExpireDate = discount.EndDate.ToDiscountFormat();

                        var discountAmount = Math.Round((price * discountRate) / 100);
                        product.PriceAfterDiscount = (price - discountAmount).ToMoney();
                    }
                }
            }


            return products;
        }


    }
}
