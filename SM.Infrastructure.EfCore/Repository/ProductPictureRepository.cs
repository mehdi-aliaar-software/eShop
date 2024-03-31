using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SM.Application.Contracts.ProductPicture;
using SM.Domain.ProductPictureAgg;

namespace SM.Infrastructure.EfCore.Repository
{
    public class ProductPictureRepository:RepositoryBase<long, ProductPicture>, IProductPictureRepository
    {
        private readonly ShopContext _context;
        public ProductPictureRepository(ShopContext context) : base(context)
        {
            _context= context;
        }

        public EditProductPicture GetDetails(long id)
        {
            var result= _context.ProductPictures.Select(x=>new EditProductPicture
            {
                Id = x.Id,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ProductId = x.ProductId
            }).FirstOrDefault(p => p.Id == id);
            return result;
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            var query = _context.ProductPictures.Include(x=>x.Product)
                        .Select(x => new ProductPictureViewModel
            {
                Id = x.Id,
                Picture = x.Picture,
                CreationDate = x.CreationDate.ToString(),
                Product = x.Product.Name,
                IsRemoved = x.IsRemoved,
                ProductId = x.ProductId,
            });

            if (searchModel.ProductId!=0)
            {
                query = query.Where(x => x.ProductId == searchModel.ProductId);
            }

            var result = query.OrderByDescending(x => x.Id).ToList();
            return result;  
        }
    }
}
