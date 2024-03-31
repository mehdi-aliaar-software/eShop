using SM.Application.Contracts.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using SM.Domain.ProductAgg;

namespace SM.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;

        public ProductApplication(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public OperationResult Create(CreateProduct command)
        {
            var result = new OperationResult();
            if (_productRepository.Exists(x => x.Name == command.Name))
            {
                return result.Failed(ApplicationMessages.DuplicatedRecord);
            }
            var product = new Product(command.Name, command.Code, command.UnitPrice, command.ShortDescription,
                command.Description, command.Picture, command.PictureAlt, command.PictureTitle, command.Slug, command.Keywords,
                command.MetaDescription, command.CategoryId);
            _productRepository.Create(product);
            _productRepository.SaveChanges();
            return result.Succeeded();
        }

        public OperationResult Edit(EditProduct command)
        {
            var result = new OperationResult();
            var product = _productRepository.GetBy(command.Id);
            if (product == null)
            {
                return result.Failed(ApplicationMessages.RecordNotFound);
            }

            if (_productRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
            {
                return result.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var slug = GenerateSlug.Slugify(command.Slug);
            product.Edit(command.Name, command.Code, command.UnitPrice, command.ShortDescription,
                command.Description, command.Picture, command.PictureAlt, command.PictureTitle, slug, command.Keywords,
                command.MetaDescription, command.CategoryId);

            _productRepository.SaveChanges();
            return result.Succeeded();
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _productRepository.Search(searchModel);
        }

        public List<ProductViewModel> GetProducts()
        {
            var result=_productRepository.GetProducts();
            return result;
        }

        public EditProduct GetDetails(long id)
        {
            return _productRepository.GetDetails(id);
        }

        public OperationResult IsStock(long id)
        {
            var result= new OperationResult();
            var product = _productRepository.GetBy(id);
            if (product == null)
            {
                return result.Failed(ApplicationMessages.RecordNotFound);
            }
            product.InStock();
            _productRepository.SaveChanges();

           return result.Succeeded();
        }

        public OperationResult NotInStock(long id)
        {
            var result = new OperationResult();
            var product = _productRepository.GetBy(id);
            if (product == null)
            {
                return result.Failed(ApplicationMessages.RecordNotFound);
            }
            product.NotInStock();
            _productRepository.SaveChanges();

            return result.Succeeded();
        }
    }
}
