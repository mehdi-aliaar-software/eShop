using SM.Application.Contracts.ProductCategory;
using System.Linq.Expressions;
using _0_Framework.Domain;

namespace SM.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository:IRepository<long, ProductCategory>
    {
        List<ProductCategoryViewModel> GetProductCategories();
        EditProductCategory GetDetails(long id);
        string GetSlugBy(long id);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}
