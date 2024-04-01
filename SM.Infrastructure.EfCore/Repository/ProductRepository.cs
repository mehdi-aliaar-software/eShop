using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Domain;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SM.Application.Contracts.Product;
using SM.Domain.ProductAgg;

namespace SM.Infrastructure.EfCore.Repository
{
    public class ProductRepository:RepositoryBase<long, Product>, IProductRepository
    {
        private readonly ShopContext _shopContext;
        public ProductRepository(ShopContext context) : base(context)
        {
            _shopContext = context;
        }

        public EditProduct GetDetails(long id)
        {

            var result= _shopContext.Products.Select(x => new EditProduct
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                Description = x.Description,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ShortDescription = x.ShortDescription,
                Slug = x.Slug,
                UnitPrice = x.UnitPrice,
                CategoryId = x.CategoryId,


            }).FirstOrDefault(x => x.Id == id);

            return result;

        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            var query = _shopContext.Products.Include(x=>x.Category).Select(x => new ProductViewModel
            {
                Id= x.Id,
                Name = x.Name,
                Code = x.Code,
                UnitPrice = x.UnitPrice,
                Category = x.Category.Name,
                Picture = x.Picture,
                CategoryId=x.CategoryId,
                CreationDate = x.CreationDate.ToFarsi(),
                IsInStock = x.IsInStock
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(x => x.Name.Contains( searchModel.Name));
            }
            if (!string.IsNullOrWhiteSpace(searchModel.Code))
            {
                query = query.Where(x => x.Code.Contains(searchModel.Code));
            }
            if (searchModel.CategoryId!=0)
            {
                query = query.Where(x => x.CategoryId== searchModel.CategoryId);
            }

            var result=  query.OrderByDescending(x => x.Id).ToList();
            return result;
        }

        public List<ProductViewModel> GetProducts()
        {
            var result = _shopContext
                .Products
                .Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name
               
            }).ToList();
            return result;
        }
    }
}
