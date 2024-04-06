using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using SM.Application.Contracts.ProductPicture;
using SM.Domain.ProductAgg;
using SM.Domain.ProductPictureAgg;

namespace SM.Application
{
    public class ProductPictureApplication: IProductPictureApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IProductPictureRepository _productPictureRepository;
        private readonly IProductRepository _productrepository;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository, IProductRepository productrepository, IFileUploader fileUploader)
        {
            _productPictureRepository = productPictureRepository;
            _productrepository = productrepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var operation = new OperationResult();

            var product = _productrepository.GetProductWithCategory(command.ProductId);
            var path = $"{product.Category.Slug}/{product.Slug}";
            var picturePath= _fileUploader.Upload(command.Picture, path);

            //if (_productPictureRepository.Exists(x=>x.Picture == command.Picture && x.ProductId == command.ProductId))
            //{
            //     return operation.Failed(ApplicationMessages.DuplicatedRecord);
            //}

            var productPicture= new ProductPicture(command.ProductId, picturePath, command.PictureAlt,
                command.PictureTitle);
            _productPictureRepository.Create(productPicture);
            _productPictureRepository.SaveChanges();
            return operation.Succeeded();

        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();

            var productPicture = _productPictureRepository.GetWithProductAndCategoryBy(command.Id);
            if (productPicture==null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            //if (_productPictureRepository
            //    .Exists(x => x.Picture == command.Picture 
            //                                        && x.ProductId == command.ProductId
            //                                        && x.Id!=command.Id))
            //{
            //    return operation.Failed(ApplicationMessages.DuplicatedRecord);
            //}

            var path = $"{productPicture.Product.Category.Slug}/{productPicture.Product.Slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);

            productPicture.Edit(command.ProductId, picturePath, command.PictureAlt,
                command.PictureTitle);


            _productPictureRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();

            var productPicture = _productPictureRepository.GetBy(id);
            if (productPicture == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            productPicture.Remove();
            _productPictureRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();

            var productPicture = _productPictureRepository.GetBy(id);
            if (productPicture == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            productPicture.Restore();
            _productPictureRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditProductPicture GetDetails(long id)
        {
            var result=_productPictureRepository.GetDetails(id);
            return result;
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            var result = _productPictureRepository.Search(searchModel);
            return result;
        }
    }
}
