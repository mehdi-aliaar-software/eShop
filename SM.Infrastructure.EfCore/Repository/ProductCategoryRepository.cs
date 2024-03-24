using SM.Domain.ProductCategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using SM.Application.Contracts.ProductCategory;

namespace SM.Infrastructure.EfCore.Repository
{
    public class ProductCategoryRepository: IProductCategoryRepository
    {
        private readonly ShopContext _shopContext;

        public ProductCategoryRepository(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public void Create(ProductCategory entity)
        {
            _shopContext.ProductCategories.Add(entity);
        }

        public ProductCategory GetBy(long id)
        {
            return _shopContext.ProductCategories.Find(id);
        }

        public List<ProductCategory> GetAll()
        {
            return _shopContext.ProductCategories.ToList();
        }

        public bool Exists(Expression<Func<ProductCategory, bool>> expression)
        {
            return _shopContext.ProductCategories.Any(expression);
        }

        public EditProductCategory GetDetails(long id)
        {
            
            return _shopContext.ProductCategories.Select(x=>new EditProductCategory
            {
                Id = x.Id, Name = x.Name, Description = x.Description, Keywords = x.Keywords, MetaDescription = x.MetaDescription,
                Picture = x.Picture, PictureAlt = x.PictureAlt, PictureTitle = x.PictureTitle, Slug = x.Slug
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            var query = _shopContext.ProductCategories.Select(x => new ProductCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                CreationDate = x.CreationDate.ToString()
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            }

            return query.OrderByDescending(x => x.Id).ToList();
        }

        public void Save()
        {
            _shopContext.SaveChanges();
        }
    }
}
