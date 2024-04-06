using SM.Application.Contracts.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using SM.Domain.ProductAgg;
using SM.Domain.ProductCategoryAgg;

namespace SM.Application
{
    
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductApplication(IProductRepository productRepository, IFileUploader fileUploader, IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            _fileUploader = fileUploader;
            _productCategoryRepository = productCategoryRepository;
        }
        public OperationResult Create(CreateProduct command)
        {
            var result = new OperationResult();
            if (_productRepository.Exists(x => x.Name == command.Name))
            {
                return result.Failed(ApplicationMessages.DuplicatedRecord);
            }
            var slug=command.Slug.Slugify();
            var categorySlug = _productCategoryRepository.GetSlugBy(command.CategoryId);
            var path = $"{categorySlug}/{slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);

            var product = new Product(command.Name, command.Code,  command.ShortDescription,
                command.Description, picturePath, command.PictureAlt, command.PictureTitle, slug, command.Keywords,
                command.MetaDescription, command.CategoryId);
            _productRepository.Create(product);
            _productRepository.SaveChanges();
            return result.Succeeded();
        }

        public OperationResult Edit(EditProduct command)
        {
            var result = new OperationResult();
            var product = _productRepository.GetProductWithCategory(command.Id);
            if (product == null)
            {
                return result.Failed(ApplicationMessages.RecordNotFound);
            }

            if (_productRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
            {
                return result.Failed(ApplicationMessages.DuplicatedRecord);
            }

            //var slug = GenerateSlug.Slugify(command.Slug);

            var slug = command.Slug.Slugify();
            var path = $"{product.Category.Slug}/{slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);

            product.Edit(command.Name, command.Code,  command.ShortDescription,command.Description, 
                picturePath, command.PictureAlt, command.PictureTitle, slug, command.Keywords,
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

    }
}
