using _0_Framework.Domain;
using SM.Application.Contracts.ProductPicture;

namespace SM.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository:IRepository<long, ProductPicture>
    {
        EditProductPicture GetDetails(long id);
        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);

    }
}
