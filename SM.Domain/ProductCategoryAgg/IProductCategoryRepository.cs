using SM.Application.Contracts.ProductCategory;
using System.Linq.Expressions;
using _0_Framework.Domain;

namespace SM.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository:IRepository<long, ProductCategory>
    {
        EditProductCategory GetDetails(long id);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}
