using _0_Framework.Application;
using SM.Application.Contracts.ProductCategory;
using SM.Domain.ProductCategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotImplementedException = System.NotImplementedException;

namespace SM.Application
{
    public class ProductCategoryApplication: IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }
        public OperationResult Create(CreateProductCategory command)
        {
            var operationResult= new OperationResult();
            if (_productCategoryRepository.Exists(x=>x.Name==command.Name))
            {
                return operationResult.Failed("امکان درج رکورد تکراری وجود ندارد ");
            }

            var slug = GenerateSlug.Slugify(command.Name);
            var productCategory = new ProductCategory(command.Name, command.Description, command.Picture,
                command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, slug);
            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operationResult = new OperationResult();

            var productCategory = _productCategoryRepository.GetBy(command.Id);
            if (productCategory == null)
            {
                return operationResult.Failed("رکورد یافت نشد");
            }

            var pr = _productCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id);
            if (pr)
            {
                return operationResult.Failed("امکان درج رکورد تکراری وجود ندارد");
            }

            string slug = GenerateSlug.Slugify(command.Name);
            productCategory.Edit(command.Name, command.Description, command.Picture, command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, slug);
            _productCategoryRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }

        public EditProductCategory GetDetails(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }



    }
}
