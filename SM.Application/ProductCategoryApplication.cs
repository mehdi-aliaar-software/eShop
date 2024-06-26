﻿using _0_Framework.Application;
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
        private readonly IFileUploader _fileUploader;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository, IFileUploader fileUploader)
        {
            _productCategoryRepository = productCategoryRepository;
            _fileUploader = fileUploader;
        }
        public OperationResult Create(CreateProductCategory command)
        {
            var operationResult= new OperationResult();
            if (_productCategoryRepository.Exists(x=>x.Name==command.Name))
            {
                return operationResult.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var slug = GenerateSlug.Slugify(command.Name);
            //var productCategory = new ProductCategory(command.Name, command.Description, command.Picture,
            //    command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, slug);
            var productCategory = new ProductCategory(command.Name, command.Description, "",
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
                return operationResult.Failed(ApplicationMessages.RecordNotFound);
            }

            var pr = _productCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id);
            if (pr)
            {
                return operationResult.Failed(ApplicationMessages.DuplicatedRecord);
            }

            string slug = GenerateSlug.Slugify(command.Name);
            var picturePath = $"{command.Slug}";
            var pictureName = _fileUploader.Upload(command.Picture, picturePath);

            //productCategory.Edit(command.Name, command.Description, command.Picture, command.PictureAlt, 
            //    command.PictureTitle, command.Keywords, command.MetaDescription, slug);
            productCategory.Edit(command.Name, command.Description, pictureName, command.PictureAlt,
                command.PictureTitle, command.Keywords, command.MetaDescription, slug);
            _productCategoryRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }

        public List<ProductCategoryViewModel> GetProductCategories()
        {
            return _productCategoryRepository.GetProductCategories();
        }

        public EditProductCategory GetDetails(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }



    }
}
